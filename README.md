# CSharp
This is C# code (often a DLL) for reference/example.

## Public.Debugging.Research
The module is written to be directly imported into PowerShell (e.g.: Import-Module Public.Debugging.Research.dll -Verbose).

### DebugDumpFile
PowerShell Command (Debug-CrashDump) written to dump the threads from a dump file.

### DebugLiveProcess
PowerShell Command (Debug-LiveProcess) written to dump the threads from a live process.

## Public.Exchange.Research
DLL meant to be imported into PowerShell via a PowerShell Script, which performs mass commands in parallel against an array of servers.

## Public.ExtensionMethods.Research
DLL/Class meant to be imported to extend types in .NET

### Veckan
Extends the System.DateTime type to return the [ISO week number](https://en.wikipedia.org/wiki/ISO_week_date#Calculating_the_week_number_of_a_given_date) from a given date.
