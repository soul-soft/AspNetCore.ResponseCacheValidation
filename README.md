# AspNetCore.ResponseCacheValidation


## QQ群：307564339

## Nuget
[EntityFrameworkCore.Extensions.SqlServer](SqlServer)
[EntityFrameworkCore.Extensions.MySql](MySql)

## 1. 基本原理

> 1. 浏览器第一次向服务器请求a资源，服务器返回该资源，并响应一个请求头etag:123456，浏览器发现etag会保存a资源
>
> 2. 浏览器第二次向服务器请求a资源，在请求头自动携带if-none-match:123456，服务器接受到该请求对比此时文件的版本号，发现版本号没有改变，则响应一个statusCode:304不响应内容，
    浏览器发现服务器响应了304会自动从本地缓存获取a资源
>   
> 3. 浏览器第二次向服务器请求a资源，在请求头自动携带if-none-match:123456，服务器接受到该请求对比此时文件的版本号，发现版本号已经改变，则响应一个请求头etag:7894551，浏览器发etag会保存a资源
>
> 4. 可见每次都会向服务器发起请求，但是服务器不一定每次都有响应内容，当图片资源或者比较大的文件资源，频繁请求可以通过该机制节约带宽


## 2. 基本使用

### 2.1 定义一个验证器，已通知浏览器是否更新缓存

``` C#
 public class WeatherForecastResponseCacheValidator
        : IResponseCacheValidator
  {
      //全局计数器
      public static int RequestCount = 0;
      public Task<string?> GetEntityHashCodeAsync(HttpContext context)
      {
          RequestCount++;
          //第三次请求模拟修改数据
          if (RequestCount % 4 == 0)
          {
              return Task.FromResult<string?>("3247f72d7f19cd79");
          }
          return Task.FromResult<string?>("b2c817f6b3f8602f");
      }
  }
```
### 2.2 给指定的endpoint添加验证器

``` C#
[HttpGet(Name = "GetWeatherForecast")]
[ResponseCacheValidation("WeatherForecast")]
public IEnumerable<WeatherForecast> Get()
{
    _logger.LogInformation("Execute 'WeatherForecast' action...");
    return Enumerable.Range(1, 5).Select(index => new WeatherForecast
    {
        Date = DateTime.Now.AddDays(index),
        TemperatureC = Random.Shared.Next(-20, 55),
        Summary = Summaries[Random.Shared.Next(Summaries.Length)]
    })
    .ToArray();
}
```

### 2.3 注册服务和启用中间件

``` C#
//1. Startup.ConfigureServices
services.AddResponseCacheValidation(options => 
{
    options.AddCacheValidator<WeatherForecastResponseCacheValidator>("WeatherForecast");
});
//2. Startup.Configure
//必须注册在 app.UseRouting()之后以便能找到endpoint并提前进行预处理
app.UseResponseCacheValidation();
```
