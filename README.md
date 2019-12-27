# AutoAssert

Removing boilerplate code to unit test argument checking.

## Packaging

The latest release-worthy version of [AutoAssert](https://www.nuget.org/packages/AutoAssert) is published to NuGet.org

Refer there for published versions and installation using your favorite package manager.

Packages target .NET Framework 4.5.2 and .NET Standard 2.0.

## Usage

In order to use a NullCheckAssert, just reference the [AutoAssert package](https://www.nuget.org/packages/AutoAssert) and do something like this:

```csharp
var assert = Asserters.GetNullCheckAsserter();
assert.AllCheckedIn<ClassThatShouldCheckForNull>();
```

If any public method or property doesn't throw for null arguments, ```AllCheckedIn<T>``` will throw.

## Builds

[![Master Branch Build Status](https://dev.azure.com/simonwendel-public/builds/_apis/build/status/simonwendel.auto-assert?branchName=master)](https://dev.azure.com/simonwendel-public/builds/_build/latest?definitionId=4&branchName=master)

Built by [Azure Pipelines](https://dev.azure.com/simonwendel-public/builds/_build?definitionId=4&_a=summary) and promoted through [Azure Artifacts to NuGet](https://dev.azure.com/simonwendel-public/builds/_release?_a=releases&view=mine&definitionId=1) when something interesting happens.
