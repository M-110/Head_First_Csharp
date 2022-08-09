namespace SecretIngredient;

class Adrian
{
    public GetSecretIngredient MySecretIngredientMethod => AddAdriansSecretIngredient;

    string AddAdriansSecretIngredient(int amount)
    {
        return $"{amount} ounces of cloves";
    }
}
