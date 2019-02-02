const { events, Job } = require("brigadier");

const projectName = "cnab-netstandard";
const dotnetImg = "microsoft/dotnet:2.2.103-sdk";

events.on("check_suite:requested", runTests);
events.on("check_suite:rerequested", runTests);
events.on("check_run:rerequested", runTests);

events.on("exec", (e, p) => {
    var job = build(e, p);
    job.run();
});

function runTests(e, p) {
    console.log("Check requested");

    var note = new Notification('tests', e, p);
    note.conclusion = "";
    note.title = "Run Tests";
    note.summary = "This test ensures the project builds, all tests pass, and the example run is successful."

    return notificationWrap(build(e, p), note);
}

function build(e, p) {
    var build = new Job(`${projectName}-build`, dotnetImg);

    build.tasks = [
        "cd /src",
        "dotnet restore",
        "dotnet build",
        "dotnet test",
        "cd examples",
        "dotnet run"
    ];

    return build;
}

// A GitHub Check Suite notification
class Notification {
    constructor(name, e, p) {
        this.proj = p;
        this.payload = e.payload;
        this.name = name;
        this.externalID = e.buildID;
        this.detailsURL = `https://azure.github.io/kashti/builds/${e.buildID}`;
        this.title = "running check";
        this.text = "";
        this.summary = "";

        // count allows us to send the notification multiple times, with a distinct pod name
        // each time.
        this.count = 0;

        // One of: "success", "failure", "neutral", "cancelled", or "timed_out".
        this.conclusion = "neutral";
    }

    // Send a new notification, and return a Promise<result>.
    run() {
        this.count++
        var j = new Job(`${this.name}-${this.count}`, "deis/brigade-github-check-run:latest");
        j.imageForcePull = true;
        j.env = {
            CHECK_CONCLUSION: this.conclusion,
            CHECK_NAME: this.name,
            CHECK_TITLE: this.title,
            CHECK_PAYLOAD: this.payload,
            CHECK_SUMMARY: this.summary,
            CHECK_TEXT: this.text,
            CHECK_DETAILS_URL: this.detailsURL,
            CHECK_EXTERNAL_ID: this.externalID
        }
        return j.run();
    }
}

// Helper to wrap a job execution between two notifications.
async function notificationWrap(job, note, conclusion) {
    if (conclusion == null) {
        conclusion = "success"
    }
    await note.run();
    try {
        let res = await job.run()
        const logs = await job.logs();

        note.conclusion = conclusion;
        note.summary = `Task "${job.name}" passed`;
        note.text = note.text = "```" + res.toString() + "```\nTest Complete";
        return await note.run();
    } catch (e) {
        const logs = await job.logs();
        note.conclusion = "failure";
        note.summary = `Task "${job.name}" failed for ${e.buildID}`;
        note.text = "```" + logs + "```\nFailed with error: " + e.toString();
        try {
            return await note.run();
        } catch (e2) {
            console.error("failed to send notification: " + e2.toString());
            console.error("original error: " + e.toString());
            return e2;
        }
    }
}