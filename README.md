# redis.net
### an efficient, binary-safe redis client library in c# 
#### Author: Ryan Petrich
#### Inspired by <a href="http://www.github.com/migueldeicaza">migueldeicaza</a>'s <a href="http://github.com/migueldeicaza/redis-sharp">redis-sharp</a>
_redis.net is in early alpha testing and needs your feedback_

## 
### Example:
    using (var connection = new RedisConnection()) {
        connection.Set("foo", "bar");
        Debug.Print("foo = {0}", (string)connection.Get("foo").Result);
    }
