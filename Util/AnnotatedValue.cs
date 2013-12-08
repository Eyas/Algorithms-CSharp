using System.Text;

namespace Util
{
    public class AnnotatedValue<TValue, TAnnotation>
    {
        public readonly TValue value;
        public readonly TAnnotation annotation;
        public AnnotatedValue(TValue v, TAnnotation a)
        {
            value = v;
            annotation = a;
        }
        public override bool Equals(object obj)
        {
            if (obj is TValue)
            {
                return value.Equals(obj);
            }
            else
            {
                AnnotatedValue<TValue, TAnnotation> other = obj as AnnotatedValue<TValue, TAnnotation>;
                return (other != null) && value.Equals(other.value);
            }
        }
        public override int GetHashCode()
        {
            return value.GetHashCode();
        }
        public override string ToString()
        {
            return (new StringBuilder()).
                Append("AnnotatedValue<").
                Append(value.ToString()).
                Append(":").
                Append(annotation.ToString()).
                Append('>').
                ToString();
        }

    }
}
