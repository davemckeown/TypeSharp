namespace TestCompileProject
{
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

        public void VoidMethod()
        {
            NumberProp++;
        }
    }
}
