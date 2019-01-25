# CSharp
This is C# code (often a DLL) for reference/example.

## Public.Activities.Research
Contains Activities that can be invoked via Windows Workflow Foundation.

### DateTimeActivity
An example Activity that returns the current DateTime in UTC.

## Public.Debugging.Research
The module is written to be directly imported into PowerShell.

```
Import-Module ".\Public.Debugging.Research.dll" -Verbose
VERBOSE: Loading module from path '.\Public.Debugging.Research.dll'.
VERBOSE: Importing cmdlet 'Debug-DumpFile'.
VERBOSE: Importing cmdlet 'Debug-LiveProcess'.
```

### DebugDumpFile
PowerShell Command (Debug-DumpFile) written to dump the threads from a dump file.

Example (redacted for brevity):
```
Debug-DumpFile -FilePath "C:\Users\Public\Downloads\Dumps\13.Jan.2019_19.18_powershell_9544.dmp.mini0"
2AD8
  dd44a7d2c8            0 mscorlib [HelperMethodFrame_1OBJ] (System.Threading.WaitHandle.WaitOneNative)
  dd44a7d3f0 7ffacb07efdb mscorlib System.Threading.WaitHandle.InternalWaitOne(System.Runtime.InteropServices.SafeHandle, Int64, Boolean, Boolean)
  dd44a7d420 7ffacb07efae mscorlib System.Threading.WaitHandle.WaitOne(Int32, Boolean)
  dd44a7d460 7ffac1c46b33 System.Management.Automation System.Management.Automation.Runspaces.PipelineBase.Invoke(System.Collections.IEnumerable)
  dd44a7d4a0 7ffac1dde7be System.Management.Automation System.Management.Automation.PowerShell+Worker.ConstructPipelineAndDoWork(System.Management.Automation.Runspaces.Runspace, Boolean)
  dd44a7d570 7ffac1dde1dd System.Management.Automation System.Management.Automation.PowerShell+Worker.CreateRunspaceIfNeededAndDoWork(System.Management.Automation.Runspaces.Runspace, Boolean)
  dd44a7d5e0 7ffac1c95048 System.Management.Automation System.Management.Automation.PowerShell.CoreInvokeHelper[[System.__Canon, mscorlib],[System.__Canon, mscorlib]](System.Management.Automation.PSDataCollection`1<System.__Canon>, System.Management.Automation.PSDataCollection`1<System.__Canon>, System.Management.Automation.PSInvocationSettings)
  dd44a7d650 7ffac1c9555a System.Management.Automation System.Management.Automation.PowerShell.CoreInvoke[[System.__Canon, mscorlib],[System.__Canon, mscorlib]](System.Management.Automation.PSDataCollection`1<System.__Canon>, System.Management.Automation.PSDataCollection`1<System.__Canon>, System.Management.Automation.PSInvocationSettings)
  dd44a7d6d0 7ffac1c9256e System.Management.Automation System.Management.Automation.PowerShell.Invoke(System.Collections.IEnumerable, System.Management.Automation.PSInvocationSettings)
  dd44a7d730 7ffaa2320816 Microsoft.PowerShell.ConsoleHost Microsoft.PowerShell.ConsoleHostUserInterface.TryInvokeUserDefinedReadLine(System.String ByRef)
  dd44a7d790 7ffaa2320018 Microsoft.PowerShell.ConsoleHost Microsoft.PowerShell.ConsoleHostUserInterface.ReadLineWithTabCompletion(Microsoft.PowerShell.Executor)
  dd44a7d850 7ffaa2323e32 Microsoft.PowerShell.ConsoleHost Microsoft.PowerShell.ConsoleHost+InputLoop.Run(Boolean)
  dd44a7d900 7ffaa23239fd Microsoft.PowerShell.ConsoleHost Microsoft.PowerShell.ConsoleHost+InputLoop.RunNewInputLoop(Microsoft.PowerShell.ConsoleHost, Boolean)
  dd44a7d950 7ffaa2317429 Microsoft.PowerShell.ConsoleHost Microsoft.PowerShell.ConsoleHost.EnterNestedPrompt()
  dd44a7d9b0 7ffaa231831c Microsoft.PowerShell.ConsoleHost Microsoft.PowerShell.ConsoleHost.DoRunspaceLoop(System.String, Boolean, System.Collections.ObjectModel.Collection`1<System.Management.Automation.Runspaces.CommandParameter>, Boolean, Boolean, System.String)
  dd44a7da30 7ffaa231819a Microsoft.PowerShell.ConsoleHost Microsoft.PowerShell.ConsoleHost.Run(Microsoft.PowerShell.CommandLineParameterParser, Boolean)
  dd44a7daa0 7ffaa2316626 Microsoft.PowerShell.ConsoleHost Microsoft.PowerShell.ConsoleHost.Start(System.Management.Automation.Runspaces.RunspaceConfiguration, System.String, System.String, System.String, System.String[])
  dd44a7db20 7ffaa232db8b Microsoft.PowerShell.ConsoleHost Microsoft.PowerShell.UnmanagedPSEntry.Start(System.String, System.String[])
  dd44a7de28            0 UNKNOWN [DebuggerU2MCatchHandlerFrame]
  dd44a7e168            0 mscorlib [HelperMethodFrame_PROTECTOBJ] (System.RuntimeMethodHandle.InvokeMethod)
  dd44a7e2e0 7ffacb0d44e0 mscorlib System.Reflection.RuntimeMethodInfo.UnsafeInvokeInternal(System.Object, System.Object[], System.Object[])
  dd44a7e350 7ffacb0bf4ee mscorlib System.Reflection.RuntimeMethodInfo.Invoke(System.Object, System.Reflection.BindingFlags, System.Reflection.Binder, System.Object[], System.Globalization.CultureInfo)
  dd44a7f440            0 UNKNOWN [DebuggerU2MCatchHandlerFrame]
  dd44a7f490            0 UNKNOWN [GCFrame]
  dd44a7f458            0 UNKNOWN [GCFrame]
2B90
  dd4533e7a8            0 mscorlib [HelperMethodFrame_1OBJ] (System.Threading.WaitHandle.WaitOneNative)
  dd4533e8d0 7ffacb07efdb mscorlib System.Threading.WaitHandle.InternalWaitOne(System.Runtime.InteropServices.SafeHandle, Int64, Boolean, Boolean)
  dd4533e900 7ffacb07efae mscorlib System.Threading.WaitHandle.WaitOne(Int32, Boolean)
  dd4533e940 7ffac7c63d14 System.Core System.IO.Pipes.NamedPipeServerStream.EndWaitForConnection(System.IAsyncResult)
  dd4533e9a0 7ffac20835dc System.Management.Automation System.Management.Automation.Remoting.RemoteSessionNamedPipeServer.ProcessListeningThread(System.Object)
  dd4533ead0 7ffacb0c3a63 mscorlib System.Threading.ExecutionContext.RunInternal(System.Threading.ExecutionContext, System.Threading.ContextCallback, System.Object, Boolean)
  dd4533eba0 7ffacb0c38f4 mscorlib System.Threading.ExecutionContext.Run(System.Threading.ExecutionContext, System.Threading.ContextCallback, System.Object, Boolean)
  dd4533ebd0 7ffacb0c38c2 mscorlib System.Threading.ExecutionContext.Run(System.Threading.ExecutionContext, System.Threading.ContextCallback, System.Object)
  dd4533ec20 7ffacba71afc mscorlib System.Threading.ThreadHelper.ThreadStart(System.Object)
  dd4533ee78            0 UNKNOWN [GCFrame]
  dd4533f1c8            0 UNKNOWN [DebuggerU2MCatchHandlerFrame]
```

### DebugLiveProcess
PowerShell Command (Debug-LiveProcess) written to dump the threads from a live process.

Example (redacted for brevity):
```
Debug-LiveProcess -Process 9992
18DC
  a3b787ce88            0 mscorlib [HelperMethodFrame_1OBJ] (System.Threading.WaitHandle.WaitOneNative)
  a3b787cfb0 7ffacb07efdb mscorlib System.Threading.WaitHandle.InternalWaitOne(System.Runtime.InteropServices.SafeHandle, Int64, Boolean, Boolean)
  a3b787cfe0 7ffacb07efae mscorlib System.Threading.WaitHandle.WaitOne(Int32, Boolean)
  a3b787d020 7ffac1c46b33 System.Management.Automation System.Management.Automation.Runspaces.PipelineBase.Invoke(System.Collections.IEnumerable)
  a3b787d060 7ffac1dde7be System.Management.Automation System.Management.Automation.PowerShell+Worker.ConstructPipelineAndDoWork(System.Management.Automation.Runspaces.Runspace, Boolean)
  a3b787d130 7ffac1dde1dd System.Management.Automation System.Management.Automation.PowerShell+Worker.CreateRunspaceIfNeededAndDoWork(System.Management.Automation.Runspaces.Runspace, Boolean)
  a3b787d1a0 7ffac1c95048 System.Management.Automation System.Management.Automation.PowerShell.CoreInvokeHelper[[System.__Canon, mscorlib],[System.__Canon, mscorlib]](System.Management.Automation.PSDataCollection`1<System.__Canon>, System.Management.Automation.PSDataCollection`1<System.__Canon>, System.Management.Automation.PSInvocationSettings)
  a3b787d210 7ffac1c9555a System.Management.Automation System.Management.Automation.PowerShell.CoreInvoke[[System.__Canon, mscorlib],[System.__Canon, mscorlib]](System.Management.Automation.PSDataCollection`1<System.__Canon>, System.Management.Automation.PSDataCollection`1<System.__Canon>, System.Management.Automation.PSInvocationSettings)
  a3b787d290 7ffac1c9256e System.Management.Automation System.Management.Automation.PowerShell.Invoke(System.Collections.IEnumerable, System.Management.Automation.PSInvocationSettings)
  a3b787d2f0 7ffaa2320816 Microsoft.PowerShell.ConsoleHost Microsoft.PowerShell.ConsoleHostUserInterface.TryInvokeUserDefinedReadLine(System.String ByRef)
  a3b787d350 7ffaa2320018 Microsoft.PowerShell.ConsoleHost Microsoft.PowerShell.ConsoleHostUserInterface.ReadLineWithTabCompletion(Microsoft.PowerShell.Executor)
  a3b787d410 7ffaa2323e32 Microsoft.PowerShell.ConsoleHost Microsoft.PowerShell.ConsoleHost+InputLoop.Run(Boolean)
  a3b787d4c0 7ffaa23239fd Microsoft.PowerShell.ConsoleHost Microsoft.PowerShell.ConsoleHost+InputLoop.RunNewInputLoop(Microsoft.PowerShell.ConsoleHost, Boolean)
  a3b787d510 7ffaa2317429 Microsoft.PowerShell.ConsoleHost Microsoft.PowerShell.ConsoleHost.EnterNestedPrompt()
  a3b787d570 7ffaa231831c Microsoft.PowerShell.ConsoleHost Microsoft.PowerShell.ConsoleHost.DoRunspaceLoop(System.String, Boolean, System.Collections.ObjectModel.Collection`1<System.Management.Automation.Runspaces.CommandParameter>, Boolean, Boolean, System.String)
  a3b787d5f0 7ffaa231819a Microsoft.PowerShell.ConsoleHost Microsoft.PowerShell.ConsoleHost.Run(Microsoft.PowerShell.CommandLineParameterParser, Boolean)
  a3b787d660 7ffaa2316626 Microsoft.PowerShell.ConsoleHost Microsoft.PowerShell.ConsoleHost.Start(System.Management.Automation.Runspaces.RunspaceConfiguration, System.String, System.String, System.String, System.String[])
  a3b787d6e0 7ffaa232db8b Microsoft.PowerShell.ConsoleHost Microsoft.PowerShell.UnmanagedPSEntry.Start(System.String, System.String[])
  a3b787d9e8            0 UNKNOWN [DebuggerU2MCatchHandlerFrame]
  a3b787dd28            0 mscorlib [HelperMethodFrame_PROTECTOBJ] (System.RuntimeMethodHandle.InvokeMethod)
  a3b787dea0 7ffacb0d44e0 mscorlib System.Reflection.RuntimeMethodInfo.UnsafeInvokeInternal(System.Object, System.Object[], System.Object[])
  a3b787df10 7ffacb0bf4ee mscorlib System.Reflection.RuntimeMethodInfo.Invoke(System.Object, System.Reflection.BindingFlags, System.Reflection.Binder, System.Object[], System.Globalization.CultureInfo)
  a3b787f000            0 UNKNOWN [DebuggerU2MCatchHandlerFrame]
  a3b787f050            0 UNKNOWN [GCFrame]
  a3b787f018            0 UNKNOWN [GCFrame]
2B40
  a3b80fed98            0 mscorlib [HelperMethodFrame_1OBJ] (System.Threading.WaitHandle.WaitOneNative)
  a3b80feec0 7ffacb07efdb mscorlib System.Threading.WaitHandle.InternalWaitOne(System.Runtime.InteropServices.SafeHandle, Int64, Boolean, Boolean)
  a3b80feef0 7ffacb07efae mscorlib System.Threading.WaitHandle.WaitOne(Int32, Boolean)
  a3b80fef30 7ffac7c63d14 System.Core System.IO.Pipes.NamedPipeServerStream.EndWaitForConnection(System.IAsyncResult)
  a3b80fef90 7ffac20835dc System.Management.Automation System.Management.Automation.Remoting.RemoteSessionNamedPipeServer.ProcessListeningThread(System.Object)
  a3b80ff0c0 7ffacb0c3a63 mscorlib System.Threading.ExecutionContext.RunInternal(System.Threading.ExecutionContext, System.Threading.ContextCallback, System.Object, Boolean)
  a3b80ff190 7ffacb0c38f4 mscorlib System.Threading.ExecutionContext.Run(System.Threading.ExecutionContext, System.Threading.ContextCallback, System.Object, Boolean)
  a3b80ff1c0 7ffacb0c38c2 mscorlib System.Threading.ExecutionContext.Run(System.Threading.ExecutionContext, System.Threading.ContextCallback, System.Object)
  a3b80ff210 7ffacba71afc mscorlib System.Threading.ThreadHelper.ThreadStart(System.Object)
  a3b80ff468            0 UNKNOWN [GCFrame]
  a3b80ff7b8            0 UNKNOWN [DebuggerU2MCatchHandlerFrame]
```

## Public.Exchange.Research
DLL meant to be imported into PowerShell via a PowerShell Script, which performs mass commands in parallel against an array of servers.

## Public.ExtensionMethods.Research
DLL/Class meant to be imported to extend types in .NET

### Veckan
Extends the [System.DateTime](https://docs.microsoft.com/en-us/dotnet/api/system.datetime?view=netframework-4.7.2) type to return the [ISO week number](https://en.wikipedia.org/wiki/ISO_week_date#Calculating_the_week_number_of_a_given_date) from a given date.

```
int weekNumber = dateTime.Veckan();
```

## Public.PowerShell.Research
DLL meant to be imported into PowerShell to expose commands.

### GetFileSizes
Command to find the files in a folder and return the relative sizes.

```
Get-FileSizes -Path "D:\Code\felsokning\CSharp\CSharp\Public.CSharp.Research\Public.PowerShell.Research\bin\Debug\" | FT -AutoSize

File                                                                                                                          Size (Bytes)
----                                                                                                                          ------------
D:\Code\felsokning\CSharp\CSharp\Public.CSharp.Research\Public.PowerShell.Research\bin\Debug\Public.PowerShell.Research.dll           6656
D:\Code\felsokning\CSharp\CSharp\Public.CSharp.Research\Public.PowerShell.Research\bin\Debug\Public.PowerShell.Research.pdb          17920
D:\Code\felsokning\CSharp\CSharp\Public.CSharp.Research\Public.PowerShell.Research\bin\Debug\System.Management.Automation.dll      7154176
```

```
Get-FileSizes -Path "D:\Code\felsokning\CSharp\CSharp\Public.CSharp.Research\Public.PowerShell.Research\bin\Debug\" -SizesInMB | FT -AutoSize

File                                                                                                                          Size (MB)
----                                                                                                                          ---------
D:\Code\felsokning\CSharp\CSharp\Public.CSharp.Research\Public.PowerShell.Research\bin\Debug\Public.PowerShell.Research.dll           0
D:\Code\felsokning\CSharp\CSharp\Public.CSharp.Research\Public.PowerShell.Research\bin\Debug\Public.PowerShell.Research.pdb           0
D:\Code\felsokning\CSharp\CSharp\Public.CSharp.Research\Public.PowerShell.Research\bin\Debug\System.Management.Automation.dll         6
```

### GetSystemUptime
Returns the total uptime of the system targeted. No parameter provided targets the current running system.
```
Get-SystemUptime

ComputerName Up Time
------------ -------
KALLIX       5.05:39:51.6560000
```

```
Get-SystemUptime -ComputerNames Kallix

ComputerName Up Time
------------ -------
KALLIX       5.05:39:51.6560000
```

## Public.WindowsWorkflow.Research
Demonstrates invoking Windows Workflows (InvokeWindowsWorkflow.cs) via PowerShell.

```
Invoke-WindowsWorkflow -AssemblyPath "D:\Code\felsokning\CSharp\CSharp\Public.CSharp.Research\Public.Activities.Research\bin\Debug\Public.Activities.Research.dll" -Type "Public.Activities.Research.DateTimeActivity"
Workflow a08f945f-8fe1-4b1b-8aad-7b8537ad65f9 Completed at 25/01/2019 00:08:09.
        Result: 25/01/2019 00:08:09
Workflow a08f945f-8fe1-4b1b-8aad-7b8537ad65f9 Unloaded.
```