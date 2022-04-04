# Automation Foundation for .NET

## Purpose
The purpose of this project is to help enable applications to perform data processing asynchronously using a multithreaded runtime. This runtime allows your applications to focus on meeting business objectives rather than wasting time writing your own background data processing agent and having to deal with thread management yourself.

Web servers often have a 60 second timeout to handle data processing and send a response back. What happens if the work your application has to perform exceeds that? This runtime is the answer to that problem.

## What this isn’t
While the runtime will manage the threading for you along with some features (like a producer/consumer implementation) it will not be a magic bullet to your specific problems. You will still need to write the code to connect the runtime to your data sources, and write the code that will process the work. It handles what occurs in between for you!



