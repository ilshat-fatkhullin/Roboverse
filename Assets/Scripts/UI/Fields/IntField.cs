namespace Assets.Scripts.UI.Fields
{
    public class IntField : RoboverseField<int>
    {
        protected override int ParseValue(string value)
        {
            if (int.TryParse(value, out int result))
            {
                return result;
            }

            return 0;
        }
    }
}