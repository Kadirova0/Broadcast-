namespace Pustok.Contracts
{
    public class BroadcastTemplate
    {
        public class Order
        {
            public class Created
            {
                public const string TITLE = "New order created";
                public const string CONTENT = $"There is a new order #{BroadcastTemplateKeyword.TRACKING_CODE} from {BroadcastTemplateKeyword.USER_FULL_NAME}";
            }

            public class BroadcastTemplateKeyword
            {
                public const string TRACKING_CODE = "{order_tracking_code}";
                public const string USER_FULL_NAME = "{user_full_name}";
            }
        }
    }
}
