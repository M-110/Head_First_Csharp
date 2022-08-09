namespace SecretIngredient;

class Harper
{
    int total = 20;
    public GetSecretIngredient MySecretIngredientMethod => AddHarpersSecretIngredient;

    string AddHarpersSecretIngredient(int amount)
    {
        if (total - amount < 0)
            return $"I don't have {amount} of cans of sardines!";
        else
        {
            total -= amount;
            return $"{amount} of cans of sardines";
        }
    }
}
