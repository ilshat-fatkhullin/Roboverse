namespace Assets.Scripts.UI.Fields
{
    public class FloatField : RoboverseField<float>
    {
        protected override float ParseValue(string value)
        {
            if (float.TryParse(value, out float result))
            {
                return result;
            }

            return 0;
        }
    }
}