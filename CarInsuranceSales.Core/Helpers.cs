using Telegram.Bot.Types.ReplyMarkups;

namespace CarInsuranceSales;

public static class Helpers
{
    public static ReplyKeyboardMarkup ToMarkup(this string[] actions)
    {
        var markup = new ReplyKeyboardMarkup
        {
            OneTimeKeyboard = true,
            ResizeKeyboard = true
        };

        foreach (var action in actions)
        {
            markup.AddButton(action);
        }

        return markup;
    }
}
