const { events, Job } = require("brigadier");
const { Notification, WrapNotification } = require("brigade-utils-test/out/exact-github");

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
    var note = new Notification('tests', e, p);
    note.conclusion = "";
    note.title = "Run Tests";
    note.summary = "This test ensures the project builds, all tests pass, and the example run is successful."

    return WrapNotification(build(e, p), note);
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