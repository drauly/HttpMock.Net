HttpMock.Net is a simple Http Server mock, usefull for integration testing. It's based on AspNetCore request handling, so you have full control on request and response.

nuget : 

![Nuget](https://img.shields.io/nuget/dt/HttpMock.Net.svg)


#### Simple example :

```csharp
var server = Server.Start(8888);

server.WhenGet("/my/url")
      .Respond(new { Prop = "my response" });
```

You can then open browser at ```http://localhost:8888/my/url``` to check the result

If you want to check that your endpoint actually receive requests :

```csharp
server.Received(1, httpContext => httpContext.Request.Path.Equals("/my/url"));
```

You can also clear all the request handler you have allready set :

```csharp
server.Clear();
```

#### Key Concept

Their is only thow methods to configure request handling, they allow doing anything you want. Others are only extensions :

```csharp
server.When(context => context.Request.Method.Equals("GET"))
      .Do(context => context.Response.WriteAsync("hello !").Wait());
```

You have full control on HttpContext, so you can do anything you want. HttpMock.Net comes with usefull extensions to simplify test code. You can then write your own and/or post PR.


#### ElasticSearch extensions

HttpMock.Net comes with extensions for elasticsearch (nuget id is HttpMock.Net.ElasticSearch)


```csharp
server.WhenSearch("my/index")
	  .ReturnDocs(new [] { new Item { Id = Guid.NewGuid() });
```

This will return your documents with elasticsearch format.
