# Livy API
A C# API facilitate access to Livy and HDInsight (Spark).

See [Livy.io](http://livy.io/) for project home page where [API documentation](https://github.com/cloudera/livy#rest-api) can be found.

See [this page](https://docs.microsoft.com/en-us/azure/hdinsight/hdinsight-apache-spark-livy-rest-interface) for more information on how to submit tasks to HDInsight cluster.

## Getting Started

First, initialise the rest clint with Spark cluster credentials:

```csharp
var creds = new NetworkCredential("username", "password", "cluster_name");

ILivyClient client = new LivyRestClient(creds);
```

note that `cluster_name` is not a full url but just an HDInsight cluster name.


