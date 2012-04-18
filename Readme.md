# PowerShell Custom Cmdelts Baseline Solution #

This solution is mainly a spike aimed to solve how to provide the proper abstractions in order to develop custom Cmdlets in a TDD fahion. 

In particular, it provides the harness to allow unit testing not only the underlying business logic but also the Cmdlet facade. For instance, it shows how to test Cmdlets parameters and verify if the proper ParameterSet was resolved. This also permits verifying that the proper parameters are resolved when chaining Cmdlets together through the pipeline.

TBD...
