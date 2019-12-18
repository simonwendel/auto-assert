# AutoAssert

Removing boilerplate code to unit test argument checking.

## Targets

Packages target .NET Framework 4.5.2 and .NET Standard 2.0.

## Usage

In order to use a NullCheckAssert, just reference the AutoAssert package and do something like this:

```csharp
var assert = Asserters.GetNullCheckAsserter();
assert.AllCheckedIn<ClassThatShouldCheckForNull>();
```

If any public method or property doesn't throw for null arguments, AllCheckedIn<T> will throw.

## Builds

[![Master Branch Build Status](https://dev.azure.com/simonwendel-public/builds/_apis/build/status/simonwendel.auto-assert?branchName=master)](https://dev.azure.com/simonwendel-public/builds/_build/latest?definitionId=4&branchName=master)