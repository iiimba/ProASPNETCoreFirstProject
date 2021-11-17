namespace GOFPatternExamples.Interpreter
{
    /// <summary>
    /// Valid Source values: aaa.., bb..
    /// Invalid Source: abc..., aab...
    /// </summary>
    class Context
    {
        public string Source { get; set; }

        public char Vocabulary { get; set; }

        public bool Result { get; set; }

        public int Position { get; set; }
    }
}
