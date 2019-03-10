# ShoppingBasket

## Running

The application is built against .net core 2.2

## General Thoughts

**Thanks for the fun challenge!**

### Async

In most cases async is used correctly and appropriately, however in some places Task.FromResult is used which isn't ideal but async on the interface makes complete sense in "real world" examples, e.g. database, distributed redis instance, etc.

### Event store like structure

The ShoppingBasket acts similar to an event aggregrate you would find in an event-based system, that is it's immutable so removals are appended instead of mutating the chain of events.

This is definitely a step away from a typical relational database approach

### Concurrency

A more production-like product would cater for concurrency concerns better, I've glossed over some of these for this sample application but commented appropriately where there might be some issues

### InMemory Databases

InMemory databases made sense for a simple application. These are abstracted away so should be trivial to replace without breaking the application.