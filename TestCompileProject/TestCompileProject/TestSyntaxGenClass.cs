namespace TestCompileProject
{
    using System;

    using TypeSharp;

    [TypeSharpCompile]
    public class TestSyntaxGenClass
    {
        public int NumberProp { get; set; }

        public string StringProp
        {
            get
            {
                return NumberProp.ToString();
            }
        }

        /// <summary>
        /// Void method as an example of a method accepting generics
        /// </summary>
        /// <typeparam name="TKey">Type of Key</typeparam>
        /// <typeparam name="TValue">Type of Value</typeparam>
        /// <param name="key">The key</param>
        /// <param name="value">The value</param>
        public void VoidMethod<TKey, TValue>(TKey key, TValue value)
        {
            NumberProp++;
        }
    }
}
