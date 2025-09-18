using System.Text;

namespace ScriptureMemorizer
{
    public class Word
    {
        private readonly string _text;
        private bool _isHidden;

        public Word(string text)
        {
            _text = text;
            _isHidden = false;
        }

        public bool IsHidden => _isHidden;

        public void Hide() => _isHidden = true;

        public void Reveal() => _isHidden = false;

        public string GetDisplayText()
        {
            if (!_isHidden) return _text;

            var sb = new StringBuilder(_text.Length);
            foreach (char c in _text)
            {
                sb.Append(char.IsLetter(c) ? '_' : c);
            }
            return sb.ToString();
        }

        public override string ToString() => _text;
    }
}
