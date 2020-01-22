# Test OData WebApi Expand Query Perfomance with Large Models

This project is used to investigate and reproduce the following issue https://github.com/OData/WebApi/issues/2001

When your OData service has a large model, queries including an `$expand` experience a considerable performance
hit in proportion to the size of the model.


## Reproducing the bug
To reproduce the performance degradation, this project simulates a large model by adding arbitrarily many functions
to the model.

To test the bug, run the server then use a client like postman to make the following requests and keep track of the time
taken to complete each request:

- `http://localhost:7678/odata/DummyObject` (normal query without `$expand`)
- `http://localhost:7678/odata/DummyObject?$expand=Field8` (query with `$expand`)

You'll notice that the second request (with `$expand`) takes orders of magnitude more time to complete.

To compare the results with a smaller model, you can remove the for-loop in the `Startup.cs` file that adds the functions,
then re-run the test above. You'll notice that request times are more comparable now. The time it takes to complete
the query with the `$expand` will increase as you increase the number of iterations in the for-loop.

## Testing a fix
To test whether a fix improves the performance, like the PR: https://github.com/OData/WebApi/pull/2014, then:
- uninstall the `Microsoft.AspNetCore.OData` package from the project
- build the branch with your fix locally
- Add the locally built `Microsoft.AspNetCore.OData.dll` as a reference to this project
- Re-run the test above and compare request times.