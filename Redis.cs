using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace Redis
{
    // redis.net
    // by Ryan Petrich
    // inspired by redis-sharp
    // license: New BSD License
    // (c) 2010 Ryan Petrich

    public static class RedisConnectionCommands
    {
        #region Connection handling

        public static RedisCommand Quit(this RedisConnection connection)
        {
            return connection.QueueCommand("QUIT");
        }

        public static RedisCommand Auth(this RedisConnection connection, string password)
        {
            return connection.QueueCommand("AUTH", password);
        }

        #endregion

        #region Commands operating on all the kind of values

        public static RedisCommand Exists(this RedisConnection connection, RedisValue key)
        {
            return connection.QueueCommand("EXISTS", key);
        }

        public static RedisCommand Del(this RedisConnection connection, params RedisValue[] keys)
        {
            RedisValue[] parameters = new RedisValue[keys.Length + 1];
            parameters[0] = "DEL";
            keys.CopyTo(parameters, 1);
            return connection.QueueCommand(keys);
        }

        public static RedisCommand Type(this RedisConnection connection, RedisValue key)
        {
            return connection.QueueCommand("TYPE", key);
        }

        public static RedisCommand Keys(this RedisConnection connection, string pattern)
        {
            return connection.QueueCommand("KEYS", pattern);
        }

        public static RedisCommand RandomKey(this RedisConnection connection)
        {
            return connection.QueueCommand("RANDOMKEY");
        }

        public static RedisCommand Rename(this RedisConnection connection, RedisValue oldkey, RedisValue newkey)
        {
            return connection.QueueCommand("RENAME", oldkey, newkey);
        }

        public static RedisCommand RenameNX(this RedisConnection connection, RedisValue oldkey, RedisValue newkey)
        {
            return connection.QueueCommand("RENAMENX", oldkey, newkey);
        }

        public static RedisCommand DBSize(this RedisConnection connection)
        {
            return connection.QueueCommand("DBSIZE");
        }

        public static RedisCommand Expire(this RedisConnection connection, RedisValue key, long seconds)
        {
            return connection.QueueCommand("EXPIRE", key, seconds);
        }

        public static RedisCommand ExpireAt(this RedisConnection connection, RedisValue key, long unixTime)
        {
            return connection.QueueCommand("EXPIREAT", key, unixTime);
        }

        public static RedisCommand ExpireAt(this RedisConnection connection, RedisValue key)
        {
            return connection.QueueCommand("TTL", key);
        }

        public static RedisCommand Select(this RedisConnection connection, long index)
        {
            return connection.QueueCommand("SELECT", index);
        }

        public static RedisCommand Move(this RedisConnection connection, RedisValue key, long dbindex)
        {
            return connection.QueueCommand("MOVE", key, dbindex);
        }

        public static RedisCommand FlushDB(this RedisConnection connection)
        {
            return connection.QueueCommand("FLUSHDB");
        }

        public static RedisCommand FlushAll(this RedisConnection connection)
        {
            return connection.QueueCommand("FLUSHALL");
        }

        #endregion

        #region Commands operating on string values

        public static RedisCommand Set(this RedisConnection connection, RedisValue key, RedisValue value)
        {
            return connection.QueueCommand("SET", key, value);
        }

        public static RedisCommand SetEx(this RedisConnection connection, RedisValue key, RedisValue time, RedisValue value)
        {
            return connection.QueueCommand("SETEX", key, time, value);
        }

        public static RedisCommand Get(this RedisConnection connection, RedisValue key)
        {
            return connection.QueueCommand("GET", key);
        }

        public static RedisCommand GetSet(this RedisConnection connection, RedisValue key, RedisValue value)
        {
            return connection.QueueCommand("GETSET", key, value);
        }

        public static RedisCommand MGet(this RedisConnection connection, params RedisValue[] keys)
        {
            RedisValue[] parameters = new RedisValue[keys.Length + 1];
            parameters[0] = "MGET";
            keys.CopyTo(parameters, 1);
            return connection.QueueCommand(parameters);
        }

        public static RedisCommand SetNX(this RedisConnection connection, RedisValue key, RedisValue value)
        {
            return connection.QueueCommand("SETNX", key, value);
        }

        public static RedisCommand MSet(this RedisConnection connection, params KeyValuePair<RedisValue, RedisValue>[] values)
        {
            RedisValue[] parameters = new RedisValue[values.Length * 2 + 1];
            parameters[0] = "MSET";
            int i = 1;
            foreach (KeyValuePair<RedisValue, RedisValue> pair in values) {
                parameters[i] = pair.Key;
                i++;
                parameters[i] = pair.Value;
                i++;
            }
            return connection.QueueCommand(parameters);
        }

        public static RedisCommand MSetNX(this RedisConnection connection, params KeyValuePair<RedisValue, RedisValue>[] values)
        {
            RedisValue[] parameters = new RedisValue[values.Length * 2 + 1];
            parameters[0] = "MSETNX";
            int i = 1;
            foreach (KeyValuePair<RedisValue, RedisValue> pair in values) {
                parameters[i] = pair.Key;
                i++;
                parameters[i] = pair.Value;
                i++;
            }
            return connection.QueueCommand(parameters);
        }

        public static RedisCommand Incr(this RedisConnection connection, RedisValue key)
        {
            return connection.QueueCommand("INCR", key);
        }

        public static RedisCommand IncrBy(this RedisConnection connection, RedisValue key, long integer)
        {
            return connection.QueueCommand("INCRBY", key, integer);
        }

        public static RedisCommand Decr(this RedisConnection connection, RedisValue key)
        {
            return connection.QueueCommand("DECR", key);
        }

        public static RedisCommand DecrBy(this RedisConnection connection, RedisValue key, long integer)
        {
            return connection.QueueCommand("DECRBY", key, integer);
        }

        #endregion

        #region Commands operating on lists

        public static RedisCommand RPush(this RedisConnection connection, RedisValue key, RedisValue value)
        {
            return connection.QueueCommand("RPUSH", key, value);
        }

        public static RedisCommand LPush(this RedisConnection connection, RedisValue key, RedisValue value)
        {
            return connection.QueueCommand("LPUSH", key, value);
        }

        public static RedisCommand LLen(this RedisConnection connection, RedisValue key)
        {
            return connection.QueueCommand("LLEN", key);
        }

        public static RedisCommand LRange(this RedisConnection connection, RedisValue key, long start, long end)
        {
            return connection.QueueCommand("LRANGE", key, start, end);
        }

        public static RedisCommand LTrim(this RedisConnection connection, RedisValue key, long start, long end)
        {
            return connection.QueueCommand("LTRIM", key, start, end);
        }

        public static RedisCommand LIndex(this RedisConnection connection, RedisValue key, long index)
        {
            return connection.QueueCommand("LINDEX", key, index);
        }

        public static RedisCommand LSet(this RedisConnection connection, RedisValue key, long index, RedisValue value)
        {
            return connection.QueueCommand("LSET", key, index, value);
        }

        public static RedisCommand LRem(this RedisConnection connection, RedisValue key, long count, RedisValue value)
        {
            return connection.QueueCommand("LREM", key, count, value);
        }

        public static RedisCommand LPop(this RedisConnection connection, RedisValue key)
        {
            return connection.QueueCommand("LPOP", key);
        }

        public static RedisCommand RPop(this RedisConnection connection, RedisValue key)
        {
            return connection.QueueCommand("RPOP", key);
        }

        public static RedisCommand BLPop(this RedisConnection connection, RedisValue key)
        {
            return connection.QueueCommand("BLPOP", key);
        }

        public static RedisCommand BRPop(this RedisConnection connection, RedisValue key)
        {
            return connection.QueueCommand("BRPOP", key);
        }

        public static RedisCommand RPopLPush(this RedisConnection connection, RedisValue srckey, RedisValue dstkey)
        {
            return connection.QueueCommand("RPOPLPUSH", srckey, dstkey);
        }

        #endregion

        #region Commands operating on sets

        public static RedisCommand SAdd(this RedisConnection connection, RedisValue key, RedisValue member)
        {
            return connection.QueueCommand("SADD", key, member);
        }

        public static RedisCommand SRem(this RedisConnection connection, RedisValue key, RedisValue member)
        {
            return connection.QueueCommand("SREM", key, member);
        }

        public static RedisCommand SPop(this RedisConnection connection, RedisValue key)
        {
            return connection.QueueCommand("SPOP", key);
        }

        public static RedisCommand SMove(this RedisConnection connection, RedisValue srckey, RedisValue dstkey, RedisValue member)
        {
            return connection.QueueCommand("SMOVE", srckey, dstkey, member);
        }

        public static RedisCommand SCard(this RedisConnection connection, RedisValue key)
        {
            return connection.QueueCommand("SCARD");
        }

        public static RedisCommand SMembers(this RedisConnection connection, RedisValue key)
        {
            return connection.QueueCommand("SMEMBERS", key);
        }

        public static RedisCommand SIsMember(this RedisConnection connection, RedisValue key, RedisValue member)
        {
            return connection.QueueCommand("SISMEMBER", key, member);
        }

        public static RedisCommand SInter(this RedisConnection connection, RedisValue[] keys)
        {
            RedisValue[] parameters = new RedisValue[keys.Length + 1];
            parameters[0] = "SINTER";
            keys.CopyTo(parameters, 1);
            return connection.QueueCommand(keys);
        }

        public static RedisCommand SInterStore(this RedisConnection connection, RedisValue dstkey, RedisValue[] keys)
        {
            RedisValue[] parameters = new RedisValue[keys.Length + 2];
            parameters[0] = "SINTERSTORE";
            parameters[1] = dstkey;
            keys.CopyTo(parameters, 2);
            return connection.QueueCommand(keys);
        }

        public static RedisCommand SUnion(this RedisConnection connection, RedisValue[] keys)
        {
            RedisValue[] parameters = new RedisValue[keys.Length + 1];
            parameters[0] = "SUNION";
            keys.CopyTo(parameters, 1);
            return connection.QueueCommand(keys);
        }

        public static RedisCommand SUnionStore(this RedisConnection connection, RedisValue dstkey, RedisValue[] keys)
        {
            RedisValue[] parameters = new RedisValue[keys.Length + 2];
            parameters[0] = "SUNIONSTORE";
            parameters[1] = dstkey;
            keys.CopyTo(parameters, 2);
            return connection.QueueCommand(keys);
        }

        public static RedisCommand SDiff(this RedisConnection connection, RedisValue[] keys)
        {
            RedisValue[] parameters = new RedisValue[keys.Length + 1];
            parameters[0] = "SDIFF";
            keys.CopyTo(parameters, 1);
            return connection.QueueCommand(keys);
        }

        public static RedisCommand SDiffStore(this RedisConnection connection, RedisValue dstkey, RedisValue[] keys)
        {
            RedisValue[] parameters = new RedisValue[keys.Length + 2];
            parameters[0] = "SDIFFSTORE";
            parameters[1] = dstkey;
            keys.CopyTo(parameters, 2);
            return connection.QueueCommand(keys);
        }

        public static RedisCommand SRandMember(this RedisConnection connection, RedisValue key)
        {
            return connection.QueueCommand("SRANDMEMBER", key);
        }

        #endregion

        #region Commands operating on sorted sets

        public static RedisCommand ZAdd(this RedisConnection connection, RedisValue key, double score, RedisValue member)
        {
            return connection.QueueCommand("ZADD", key, score, member);
        }

        public static RedisCommand ZRem(this RedisConnection connection, RedisValue key, RedisValue member)
        {
            return connection.QueueCommand("ZREM", key, member);
        }

        public static RedisCommand ZIncrBy(this RedisConnection connection, RedisValue key, double increment, RedisValue member)
        {
            return connection.QueueCommand("ZINCRBY", key, increment, member);
        }

        public static RedisCommand ZRank(this RedisConnection connection, RedisValue key, RedisValue member)
        {
            return connection.QueueCommand("ZRANK", key, member);
        }

        public static RedisCommand ZRevRank(this RedisConnection connection, RedisValue key, RedisValue member)
        {
            return connection.QueueCommand("ZREVRANK", key, member);
        }

        public static RedisCommand ZRange(this RedisConnection connection, RedisValue key, long start, long end)
        {
            return connection.QueueCommand("ZRANGE", key, start, end);
        }

        public static RedisCommand ZRange(this RedisConnection connection, RedisValue key, long start, long end, bool withScores)
        {
            return withScores
                ? connection.QueueCommand("ZRANGE", key, start, end, "WITHSCORES")
                : connection.QueueCommand("ZRANGE", key, start, end);
        }

        public static RedisCommand ZRevRange(this RedisConnection connection, RedisValue key, long start, long end)
        {
            return connection.QueueCommand("ZREVRANGE", key, start, end);
        }

        public static RedisCommand ZRevRange(this RedisConnection connection, RedisValue key, long start, long end, bool withScores)
        {
            return withScores
                ? connection.QueueCommand("ZREVRANGE", key, start, end, "WITHSCORES")
                : connection.QueueCommand("ZREVRANGE", key, start, end);
        }

        public static RedisCommand ZRangeByScore(this RedisConnection connection, RedisValue key, double min, double max)
        {
            return connection.QueueCommand("ZRANGEBYSCORE", key, min, max);
        }

        public static RedisCommand ZRangeByScore(this RedisConnection connection, RedisValue key, double min, double max, long offset, long count)
        {
            return connection.QueueCommand("ZRANGEBYSCORE", key, min, max, offset, count);
        }

        public static RedisCommand ZRangeByScore(this RedisConnection connection, RedisValue key, double min, double max, bool withScores)
        {
            return withScores
                ? connection.QueueCommand("ZRANGEBYSCORE", key, min, max, "WITHSCORES")
                : connection.QueueCommand("ZRANGEBYSCORE", key, min, max);
        }

        public static RedisCommand ZRangeByScore(this RedisConnection connection, RedisValue key, double min, double max, long offset, long count, bool withScores)
        {
            return withScores
                ? connection.QueueCommand("ZRANGEBYSCORE", key, min, max, offset, count, "WITHSCORES")
                : connection.QueueCommand("ZRANGEBYSCORE", key, min, max, offset, count);
        }

        public static RedisCommand ZCard(this RedisConnection connection, RedisValue key)
        {
            return connection.QueueCommand("ZCARD", key);
        }

        public static RedisCommand ZScore(this RedisConnection connection, RedisValue key, RedisValue element)
        {
            return connection.QueueCommand("ZSCORE", key, element);
        }

        public static RedisCommand ZRemRangeByRank(this RedisConnection connection, RedisValue key, long start, long end)
        {
            return connection.QueueCommand("ZREMRANGEBYRANK", key, start, end);
        }

        public static RedisCommand ZRemRangeByScore(this RedisConnection connection, RedisValue key, double start, double end)
        {
            return connection.QueueCommand("ZREMRANGEBYSCORE", key, start, end);
        }

        // TODO: implement WEIGHTS and AGGREGATE
        public static RedisCommand ZInter(this RedisConnection connection, RedisValue dstKey, RedisValue[] keys)
        {
            RedisValue[] parameters = new RedisValue[keys.Length + 2];
            parameters[0] = "ZINTER";
            parameters[1] = dstKey;
            keys.CopyTo(parameters, 2);
            return connection.QueueCommand(keys);
        }

        // TODO: implement WEIGHTS and AGGREGATE
        public static RedisCommand ZUnion(this RedisConnection connection, RedisValue dstKey, RedisValue[] keys)
        {
            RedisValue[] parameters = new RedisValue[keys.Length + 2];
            parameters[0] = "ZUNION";
            parameters[1] = dstKey;
            keys.CopyTo(parameters, 2);
            return connection.QueueCommand(keys);
        }

        #endregion

        #region Commands operating on hashes

        public static RedisCommand HSet(this RedisConnection connection, RedisValue key, RedisValue field, RedisValue value)
        {
            return connection.QueueCommand("HSET", key, field, value);
        }

        public static RedisCommand HGet(this RedisConnection connection, RedisValue key, RedisValue field)
        {
            return connection.QueueCommand("HGET", key, field);
        }

        public static RedisCommand HDel(this RedisConnection connection, RedisValue key, RedisValue field)
        {
            return connection.QueueCommand("HDEL", key, field);
        }

        public static RedisCommand HExists(this RedisConnection connection, RedisValue key, RedisValue field)
        {
            return connection.QueueCommand("HEXISTS", key, field);
        }

        public static RedisCommand HLen(this RedisConnection connection, RedisValue key)
        {
            return connection.QueueCommand("HLEN", key);
        }

        public static RedisCommand HKeys(this RedisConnection connection, RedisValue key)
        {
            return connection.QueueCommand("HKEYS", key);
        }

        public static RedisCommand HVals(this RedisConnection connection, RedisValue key)
        {
            return connection.QueueCommand("HVALS", key);
        }

        public static RedisCommand HGetAll(this RedisConnection connection, RedisValue key)
        {
            return connection.QueueCommand("HGETALL", key);
        }

        #endregion

        #region Sorting

        // TODO: Add BY, LIMIT, GET, ASC|DESC ALPHA and STORE options
        public static RedisCommand Sort(this RedisConnection connection, RedisValue key)
        {
            return connection.QueueCommand("SORT", key);
        }

        #endregion

        #region Transactions

        public static RedisCommand Multi(this RedisConnection connection)
        {
            return connection.QueueCommand("MULTI");
        }

        public static RedisCommand Exec(this RedisConnection connection)
        {
            return connection.QueueCommand("EXEC");
        }

        public static RedisCommand Discard(this RedisConnection connection)
        {
            return connection.QueueCommand("DISCARD");
        }

        #endregion

        #region Publish/Subscribe

        public static RedisCommand Subscribe(this RedisConnection connection, params RedisValue[] classes)
        {
            RedisValue[] parameters = new RedisValue[classes.Length + 1];
            parameters[0] = "SUBSCRIBE";
            classes.CopyTo(parameters, 1);
            return connection.QueueCommand(parameters);
        }

        public static RedisCommand Unsubscribe(this RedisConnection connection, params RedisValue[] classes)
        {
            RedisValue[] parameters = new RedisValue[classes.Length + 1];
            parameters[0] = "UNSUBSCRIBE";
            classes.CopyTo(parameters, 1);
            return connection.QueueCommand(parameters);
        }

        public static RedisCommand Unsubscribe(this RedisConnection connection)
        {
            return connection.QueueCommand("UNSUBSCRIBE");
        }

        public static RedisCommand Publish(this RedisConnection connection, RedisValue class_, RedisValue message)
        {
            return connection.QueueCommand("PUBLISH", class_, message);
        }

        #endregion

        #region Persistence control commands

        public static RedisCommand Save(this RedisConnection connection)
        {
            return connection.QueueCommand("SAVE");
        }

        public static RedisCommand BgSave(this RedisConnection connection)
        {
            return connection.QueueCommand("BGSAVE");
        }

        public static RedisCommand LastSave(this RedisConnection connection)
        {
            return connection.QueueCommand("LASTSAVE");
        }

        public static RedisCommand Shutdown(this RedisConnection connection)
        {
            return connection.QueueCommand("SHUTDOWN");
        }

        public static RedisCommand BgRewriteAOF(this RedisConnection connection)
        {
            return connection.QueueCommand("BGREWRITEAOF");
        }

        #endregion

        #region Remote server control commands

        public static RedisCommand Info(this RedisConnection connection)
        {
            return connection.QueueCommand("INFO");
        }

        public static RedisCommand Monitor(this RedisConnection connection)
        {
            throw new NotImplementedException();
        }

        public static RedisCommand SlaveOf(this RedisConnection connection, string host, long port)
        {
            return connection.QueueCommand("SLAVEOF", host, port);
        }

        public static RedisCommand SlaveOfNoOne(this RedisConnection connection)
        {
            return connection.QueueCommand("SLAVEOF", "no", "one");
        }

        #endregion
    }

    public sealed class RedisConnectionSettings
    {
        public RedisConnectionSettings()
        {
            Host = "localhost";
            Port = 6379;
            SendTimeout = -1;
            NoDelay = true;
            SendAllOnRead = true;
            WriteBufferSize = 1024 * 4;
            ReadBufferSize = 1024 * 4;
        }
        public ushort Port { get; set; }
        public string Host { get; set; }
        public int SendTimeout { get; set; }
        public bool NoDelay { get; set; }
        public bool SendAllOnRead { get; set; }
        public int WriteBufferSize { get; set; }
        public int ReadBufferSize { get; set; }
    }

    public sealed class RedisConnection : IDisposable
    {
        readonly Socket socket;
        readonly NetworkStream stream;
        readonly BufferedStream rstream;
        readonly BufferedStream wstream;
        readonly Queue<RedisCommand> sendQueue;
        readonly Queue<RedisCommand> receiveQueue;
        readonly bool sendAllOnRead;

        #region Creation/Destruction
        public RedisConnection()
            : this(new RedisConnectionSettings())
        {
        }
        
        public RedisConnection(RedisConnectionSettings settings)
        {
            sendQueue = new Queue<RedisCommand>();
            receiveQueue = new Queue<RedisCommand>();
            sendAllOnRead = settings.SendAllOnRead;
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.NoDelay = settings.NoDelay;
            socket.SendTimeout = settings.SendTimeout;
            socket.Connect(settings.Host, settings.Port);
            if (!socket.Connected) {
                socket.Close();
                throw new IOException("Unable to connect!");
            }
            try {
                stream = new NetworkStream(socket);
                rstream = new BufferedStream(stream, settings.ReadBufferSize);
                wstream = new BufferedStream(stream, settings.WriteBufferSize);
            } catch {
                socket.Close();
                throw;
            }
        }

        public void Dispose()
        {
            try {
                SendAllCommands();
                ReadAllResults();
            } finally {
                try {
                    wstream.Dispose();
                } finally {
                    try {
                        rstream.Dispose();
                    } finally {
                        try {
                            stream.Dispose();
                        } finally {
                            socket.Close();
                        }
                    }
                }
            }
        }
        #endregion

        #region Command handling
        public RedisCommand QueueCommand(params RedisValue[] elements)
        {
            RedisCommand command = new RedisCommand(this, elements);
            sendQueue.Enqueue(command);
            return command;
        }

        public void SendAllCommands()
        {
            while (sendQueue.Count != 0) {
                RedisCommand command = sendQueue.Dequeue();
                command.WriteTo(wstream);
                receiveQueue.Enqueue(command);
            }
            wstream.Flush();
        }

        public void ReadAllResults()
        {
            SendAllCommands();
            while (receiveQueue.Count != 0)
                receiveQueue.Dequeue().WriteTo(wstream);
        }

        internal void SendCommand(RedisCommand command)
        {
            if (sendQueue.Contains(command)) {
                RedisCommand dequeued;
                do {
                    dequeued = sendQueue.Dequeue();
                    dequeued.WriteTo(wstream);
                    receiveQueue.Enqueue(dequeued);
                } while (command != dequeued);
                wstream.Flush();
            }
        }

        internal void ReadResultForCommand(RedisCommand command)
        {
            if (sendQueue.Contains(command)) {
                if (sendAllOnRead)
                    SendAllCommands();
                else
                    SendCommand(command);
            }
            if (receiveQueue.Contains(command)) {
                RedisCommand dequeued;
                do {
                    dequeued = receiveQueue.Dequeue();
                    dequeued.ReadFrom(rstream);
                } while (command != dequeued);
            }
        }
        #endregion

    }

    public sealed class RedisCommand
    {
        readonly RedisConnection connection;
        readonly RedisValue[] elements;
        RedisValue? result;

        internal RedisCommand(RedisConnection connection, params RedisValue[] elements)
        {
            this.connection = connection;
            this.elements = elements;
        }

        internal void WriteTo(Stream stream)
        {
            RedisValue value = new RedisValue() { Type = RedisValueType.MultiBulk, MultiBulkValues = elements };
            value.WriteTo(stream);
        }

        internal void ReadFrom(Stream stream)
        {
            result = RedisValue.ReadFrom(stream);
        }

        public void Send()
        {
            connection.SendCommand(this);
        }

        public void ReadResult()
        {
            if (!result.HasValue) {
                connection.ReadResultForCommand(this);
            }
        }

        public RedisValue Result
        {
            get
            {
                ReadResult();
                return result.Value;
            }
        }

        public static implicit operator RedisValue(RedisCommand command)
        {
            return command.Result;
        }

    }

    public enum RedisValueType : byte
    {
        Error = (byte)'-',
        Success = (byte)'+',
        Bulk = (byte)'$',
        MultiBulk = (byte)'*',
        Integer = (byte)':'
    }

    public struct RedisValue : IEnumerable<RedisValue>
    {
        #region Stream Helpers
        static readonly Encoding encoding = Encoding.UTF8;
        static void WriteNewline(Stream stream)
        {
            stream.WriteByte((byte)'\r');
            stream.WriteByte((byte)'\n');
        }

        static void WriteFollowedByNewline(Stream stream, byte[] buffer)
        {
            stream.Write(buffer, 0, buffer.Length);
            WriteNewline(stream);
        }

        static void WriteType(Stream stream, RedisValueType valueType)
        {
            stream.WriteByte((byte)valueType);
        }

        static byte[] ReadLine(Stream stream)
        {
            using (MemoryStream ms = new MemoryStream()) {
                for (;;) {
                    int b = stream.ReadByte();
                    switch (b) {
                        case -1:
                            throw NewStreamEndedException();
                        case '\r':
                            continue;
                        case '\n':
                            return ms.ToArray();
                        default:
                            ms.WriteByte((byte)b);
                            break;
                    }
                }
            }
        }

        static byte[] ReadToLengthOrFail(Stream stream, int length)
        {
            byte[] result = new byte[length];
            int offset = 0;
            while (length > 0) {
                int read = stream.Read(result, offset, length);
                if (read <= 0)
                    throw NewStreamEndedException();
                offset += read;
                length -= read;
            }
            return result;
        }

        static Exception NewStreamEndedException()
        {
            return new IOException("Stream ended unexpectedly!");
        }
        #endregion

        #region Buffer Conversions
        static string BufferToString(byte[] buffer)
        {
            return encoding.GetString(buffer);
        }
        static byte[] StringToBuffer(string str)
        {
            return encoding.GetBytes(str);
        }

        static long BufferToLong(byte[] buffer)
        {
            return long.Parse(BufferToString(buffer));
        }
        static byte[] LongToBuffer(long integer)
        {
            return StringToBuffer(integer.ToString());
        }

        static double BufferToDouble(byte[] buffer)
        {
            return long.Parse(BufferToString(buffer));
        }
        static byte[] DoubleToBuffer(double dbl)
        {
            return StringToBuffer(dbl.ToString());
        }
        #endregion

        #region Properties
        private RedisValueType type;
        private RedisValue[] multiBulkValues;
        private byte[] data;

        void CheckForError()
        {
            if (type == RedisValueType.Error)
                throw new InvalidOperationException("Redis error: " + BufferToString(data));
        }


        public RedisValueType Type
        {
            get { return type; }
            set { type = value; }
        }

        public byte[] Data
        {
            get
            {
                CheckForError();
                return data; 
            }
            set
            {
                multiBulkValues = null;
                data = value;
            }
        }

        public RedisValue[] MultiBulkValues
        {
            get
            {
                CheckForError();
                return multiBulkValues;
            }
            set
            {
                data = null;
                multiBulkValues = value;
            }
        }

        public string Text
        {
            get { return BufferToString(Data); }
            set { Data = StringToBuffer(value); }
        }

        public string ErrorText
        {
            get { return BufferToString(data); }
            set { Data = StringToBuffer(value); }
        }

        public long Integer
        {
            get { return BufferToLong(Data); }
            set { Data = LongToBuffer(value); }
        }

        public double Double
        {
            get { return BufferToDouble(Data); }
            set { Data = DoubleToBuffer(value); }
        }

        #endregion

        #region Conversions

        public static implicit operator long(RedisValue value)
        {
            return value.Integer;
        }
        public static implicit operator RedisValue(long value)
        {
            return new RedisValue() { Type = RedisValueType.Integer, Integer = value };
        }

        public static implicit operator double(RedisValue value)
        {
            return value.Integer;
        }
        public static implicit operator RedisValue(double value)
        {
            return new RedisValue() { Type = RedisValueType.Integer, Double = value };
        }

        public static implicit operator string(RedisValue value)
        {
            return value.Text;
        }
        public static implicit operator RedisValue(string value)
        {
            return new RedisValue() { Type = RedisValueType.Bulk, Text = value };
        }

        public static implicit operator byte[](RedisValue value)
        {
            return value.Data;
        }
        public static implicit operator RedisValue(Byte[] value)
        {
            return new RedisValue() { Type = RedisValueType.Bulk, Data = value };
        }

        public static RedisValue Error(string errorText)
        {
            return new RedisValue() { Type = RedisValueType.Error, ErrorText = errorText };
        }

        public static RedisValue Success(string errorText)
        {
            return new RedisValue() { Type = RedisValueType.Success, ErrorText = errorText };
        }

        #endregion

        #region Serialization
        internal void WriteTo(Stream stream)
        {
            switch (type) {
                case RedisValueType.Error:
                case RedisValueType.Success:
                case RedisValueType.Integer:
                    WriteType(stream, type);
                    WriteFollowedByNewline(stream, data);
                    break;
                case RedisValueType.Bulk:
                    WriteType(stream, type);
                    WriteFollowedByNewline(stream, LongToBuffer(data.Length));
                    WriteFollowedByNewline(stream, data);
                    break;
                case RedisValueType.MultiBulk:
                    WriteType(stream, type);
                    WriteFollowedByNewline(stream, LongToBuffer(multiBulkValues.Length));
                    foreach (RedisValue child in multiBulkValues) {
                        child.WriteTo(stream);
                    }
                    break;
                default:
                    throw new InvalidDataException("Unknown value type!");
            }
        }

        internal static RedisValue ReadFrom(Stream stream)
        {
            int type = stream.ReadByte();
            long length;
            RedisValue result = new RedisValue();
            switch (type) {
                case -1:
                    throw NewStreamEndedException();
                case (int)RedisValueType.Error:
                case (int)RedisValueType.Success:
                case (int)RedisValueType.Integer:
                    result.data = ReadLine(stream);
                    break;
                case (int)RedisValueType.Bulk:
                    length = BufferToLong(ReadLine(stream));
                    result.data = ReadToLengthOrFail(stream, (int)length);
                    stream.ReadByte();
                    stream.ReadByte();
                    break;
                case (int)RedisValueType.MultiBulk:
                    length = BufferToLong(ReadLine(stream));
                    if (length >= 0) {
                        RedisValue[] multiBulkElements = new RedisValue[length];
                        for (long i = 0; i < length; i++) {
                            multiBulkElements[i] = ReadFrom(stream);
                        }
                        result.multiBulkValues = multiBulkElements;
                    }
                    break;
                default:
                    throw new InvalidDataException("Unknown value type!");
            }
            result.type = (RedisValueType)type;
            return result;
        }
        #endregion

        #region IEnumerable<RedisValue>
        public IEnumerator<RedisValue> GetEnumerator()
        {
            if (multiBulkValues != null)
                for (long i = 0, length = multiBulkValues.Length; i < length; i++)
                    yield return multiBulkValues[i];
        }
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion

        #region ToString()
        private string GetASCIIStringOrDefault(string defaultString)
        {
            try {
                return Encoding.ASCII.GetString(data, 0, data.Length);
            } catch {
                return defaultString;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append((char)type);
            switch (type) {
                case RedisValueType.Error:
                case RedisValueType.Success:
                case RedisValueType.Integer:
                    sb.AppendLine(GetASCIIStringOrDefault("(BINARY/UNICODE DATA)"));
                    break;
                case RedisValueType.Bulk:
                    sb.AppendLine(data.Length.ToString());
                    sb.AppendLine(GetASCIIStringOrDefault("(BINARY/UNICODE DATA)"));
                    break;
                case RedisValueType.MultiBulk:
                    sb.AppendLine(data.Length.ToString());
                    foreach (RedisValue child in multiBulkValues) {
                        sb.AppendLine(child.ToString());
                    }
                    break;
                default:
                    throw new InvalidDataException("Unknown value type!");
            }
            sb.Remove(sb.Length - 2, 2);
            return sb.ToString();
        }
        #endregion
    }
}
