.PHONY: build
build:
	dotnet build

.PHONY: test
test:
	dotnet test

.PHONY: clean
clean:
	find . -iname "bin" | xargs rm -rf
	find . -iname "obj" | xargs rm -rf
