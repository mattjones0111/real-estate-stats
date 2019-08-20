## Real Estate Agent stats

### Overview

Here is my solution to the coding exercise.

### Method

1. I transformed the json response from the API into C# classes, removing
all the properties that were not required for the task.

2. Built a simple Api client that retrieves documents, deserializes the json.

3. Built an extension method that does the aggregation on the results.

4. Added Polly to add wait-and-retry behaviour with an exponential backoff if 
the Api request is denied because of rate limiting.

### // TODO

The solution prioritised speed of delivery over neatness; I wanted to get a 
solution done as soon as possible and if I were to spend more time I would have 
done some refactoring and separated some of the responsibilities, such as...

1. Refactor the FundaDocumentProvider further to separate the retrial from the 
main http request path by using a decorator pattern to add the retry policy.

2. Improve memory usage. Currently all the documents are loaded and mapped into 
objects that are held in memory, whereas the requirement just asks for 
aggregate numbers. I could improve this to simply update aggregate counts as 
the documents are retrieved, rather than loading everything and aggregating 
with `GroupBy` and `OrderBy`.

3. The test suite is not comprehensive; I could add tests that test the non-happy 
path and of the output formatter.

4. Breakout settings such as the Api key into config.

Matt Jones
20th August 2019
