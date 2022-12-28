using System;
using System.Reflection;
using BenchmarkDotNet.Attributes;

namespace dotnet31
{
    [MemoryDiagnoser]
    public class BaseTest
    {
        private readonly Animal animal = new Animal("dog");
        private readonly object?[] param = new object?[] { "bird" };

        private readonly MethodInfo methodInfo =
            typeof(Animal).GetMethod("SetName", BindingFlags.NonPublic | BindingFlags.Instance);

        private readonly ConstructorInfo _c = typeof(Animal).GetConstructor(
            BindingFlags.NonPublic | BindingFlags.Instance, null,
            new Type[] { typeof(string) }, null);
        
        
        [Benchmark]
        public string GetName()
        {
            return (string)typeof(Animal).GetMethod("GetName", BindingFlags.NonPublic | BindingFlags.Instance)
                .Invoke(animal, Array.Empty<object?>());
        }
        
        [Benchmark]
        public void SetName()
        {
            _ = (string)typeof(Animal).GetMethod("SetName", BindingFlags.NonPublic | BindingFlags.Instance)
                .Invoke(animal, new object[]{"bird"});
        }
        
        [Benchmark]
        public void SetCacheName()
        {
            _ = methodInfo.Invoke(animal, param);
        }
        
        [Benchmark]
        public string GetCacheName()
        {
            return (string)methodInfo.Invoke(animal, Array.Empty<object?>());
        }

        [Benchmark]
        public Animal GetAnimal()
        {
            return (Animal)typeof(Animal).GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null,
                    new Type[] { typeof(string) }, null)
                .Invoke(new object?[] { "cattwo" });
        }
        
        [Benchmark]
        public Animal GetCacheAnimal()
        {
            return (Animal)_c.Invoke(new object?[] { "cattwo" });
        }
        
    }
}