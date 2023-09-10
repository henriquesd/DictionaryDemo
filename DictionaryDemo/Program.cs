using System.Collections.Concurrent;

Console.WriteLine("Dictionary Demo\n");

// Declaring and initializing a Dictionary:
Dictionary<int, string> paymentMethods = new Dictionary<int, string>();

// Declaring and initializing a ConcurrentDictionary:
ConcurrentDictionary<int, string> paymentMethodsConcurrentDictionary = new ConcurrentDictionary<int, string>();

AddPaymentMethods(paymentMethods);
TryAddPaymentMethods(paymentMethods);
DisplayDictionaryItems(paymentMethods);
IterateWithKeysAndValues(paymentMethods);
SearchValueByExistentKey(paymentMethods);
SearchByValue(paymentMethods);
//SearchByNonExistentKey(paymentMethods);
SafelySearchByKeyWithTryGetValue(paymentMethods);
SafelySearchByKeyWithContains(paymentMethods);
SearchByKeyWithLinq(paymentMethods);
SearchByValueWithLinq(paymentMethods);
ContainsValue(paymentMethods);
UpdateValueOfAnItemByKey(paymentMethods);
RecriateItem(paymentMethods);
DisplayDictionaryItems(paymentMethods);
DictionarySize(paymentMethods);
RemoveDictionaryItem(paymentMethods);

TryAddPaymentMethodsConcurrentDictionary(paymentMethodsConcurrentDictionary);
DisplayDictionaryItemsConcurrentDictionary(paymentMethodsConcurrentDictionary);
TryGetConcurrentDictionary(paymentMethodsConcurrentDictionary);
AddOrUpdateItemConcurrentDictionary(paymentMethodsConcurrentDictionary);
TryRemoveConcurrentDictionary(paymentMethodsConcurrentDictionary);

static void AddPaymentMethods(Dictionary<int, string> paymentMethods)
{
    paymentMethods.Add(1, "Credit Card");
    paymentMethods.Add(2, "PayPal");
    paymentMethods.Add(3, "Google Pay");
}

static void TryAddPaymentMethods(Dictionary<int, string> paymentMethods)
{
    Console.WriteLine("Try add:");

    var cashAdded = paymentMethods.TryAdd(3, "Cash");
    var bitcoinAdded = paymentMethods.TryAdd(4, "Bitcoin");

    Console.WriteLine($"Was Cash added? {cashAdded}");
    Console.WriteLine($"Was Bitcoin added? {bitcoinAdded}");

    Console.WriteLine();
}

static void DisplayDictionaryItems(Dictionary<int, string> paymentMethods)
{
    foreach (var paymentMethod in paymentMethods)
    {
        Console.WriteLine($"Key: {paymentMethod.Key}, Value: {paymentMethod.Value}");
    }

    Console.WriteLine();
}

static void IterateWithKeysAndValues(Dictionary<int, string> paymentMethods)
{
    Console.WriteLine("Iterate with Keys and Values:");

    var keys = paymentMethods.Keys;

    foreach (var item in keys)
    {
        Console.WriteLine($"{item}");
    }

    var values = paymentMethods.Values;

    foreach (var item in values)
    {
        Console.WriteLine($"{item}");
    }

    Console.WriteLine();
}

static void SearchValueByExistentKey(Dictionary<int, string> paymentMethods)
{
    Console.WriteLine("Search Value by existent Key:");

    var keyToSearch = 2;

    var paymentMethod = paymentMethods[keyToSearch];

    Console.WriteLine($"Payment Method with key {keyToSearch}: {paymentMethod}");

    Console.WriteLine();
}

static void SearchByValue(Dictionary<int, string> paymentMethods)
{
    Console.WriteLine("Search by Value:");

    var valueToSearch = "PayPal";

    // Find keys by searching by the target value:
    var keysWithValue = paymentMethods
        .Where(kv => kv.Value == valueToSearch)
        .Select(kv => kv.Key)
        .ToList();

    foreach (var key in keysWithValue)
    {
        Console.WriteLine($"Key with value {valueToSearch}: {key}");
    }

    Console.WriteLine();
}

static void SearchByNonExistentKey(Dictionary<int, string> paymentMethods)
{
    // The code below will throw a KeyNotFoundException:
    var paymentMethod = paymentMethods[5];

    Console.WriteLine(paymentMethod);
}

static void SafelySearchByKeyWithTryGetValue(Dictionary<int, string> paymentMethods)
{
    Console.WriteLine("Safely search by Key with TryGetValue:");

    // The code below will not throw error when the Key does not exist:
    var keyToSearch = 5;

    if (paymentMethods.TryGetValue(keyToSearch, out string resultValue))
    {
        Console.WriteLine($"Key {keyToSearch} was found. The target value is: {resultValue}");
    }
    else
    {
        Console.WriteLine($"The Key {keyToSearch} was not found");
    }

    Console.WriteLine();
}

static void SafelySearchByKeyWithContains(Dictionary<int, string> paymentMethods)
{
    Console.WriteLine("Safely search by Key with Contains:");

    // The code below will not throw error when the Key does not exist:
    var keyToSearch = 4;

    if (paymentMethods.ContainsKey(keyToSearch))
    {
        var paymentMethod = paymentMethods[keyToSearch];
        Console.WriteLine($"Key {keyToSearch} was found. The target value is: {paymentMethod}");
    }
    else
    {
        Console.WriteLine($"The Key {keyToSearch} was not found");
    }

    Console.WriteLine();
}

static void SearchByKeyWithLinq(Dictionary<int, string> paymentMethods)
{
    Console.WriteLine("Search by Key with LINQ");

    var keyToSearch = 2; // for an existent key;
    // var keyToSearch = 6; // for a non existent key;

    var paymentMethod = paymentMethods
        .Where(pair => pair.Key == keyToSearch)
        .Select(pair => pair.Value)
        .FirstOrDefault();

    if (paymentMethod != null)
    {
        Console.WriteLine($"Payment method {keyToSearch}: {paymentMethod}");
    }
    else
    {
        Console.WriteLine($"Payment method {keyToSearch} was not found");
    }

    Console.WriteLine();
}

static void SearchByValueWithLinq(Dictionary<int, string> paymentMethods)
{
    Console.WriteLine("Search by Value with LINQ");

    // Adding another item with same Value for demonstration purpose;
    paymentMethods.TryAdd(5, "PayPal");

    //var valueToSearch = "PayPal"; // for an existent value;
    var valueToSearch = "Cash"; // for a non existent value;

    var resultPaymentMethods = paymentMethods
        .Where(pair => pair.Value == valueToSearch)
        .Select(pair => pair.Key)
        .ToList();

    if (resultPaymentMethods.Any())
    {
        Console.WriteLine($"Payment method(s) named '{valueToSearch}' found with key(s):" +
            $" {string.Join(", ", resultPaymentMethods)}");
    }
    else
    {
        Console.WriteLine($"Payment method '{valueToSearch}' was not found");
    }

    // Remove the previous added item;
    paymentMethods.Remove(5);

    Console.WriteLine();
}

static void ContainsValue(Dictionary<int, string> paymentMethods)
{
    Console.WriteLine("Contains Value:");

    var containsKey1 = paymentMethods.ContainsKey(1);
    var containsKey5 = paymentMethods.ContainsKey(5);
    Console.WriteLine($"Contains Key 1: {containsKey1}");
    Console.WriteLine($"Contains Key 5: {containsKey5}");

    var containsValuePayPal = paymentMethods.ContainsValue("PayPal");
    var containsValueCash = paymentMethods.ContainsValue("Cash");
    Console.WriteLine($"Contains Value PayPal: {containsValuePayPal}");
    Console.WriteLine($"Contains Value Cash: {containsValueCash}");

    Console.WriteLine();
}

static void UpdateValueOfAnItemByKey(Dictionary<int, string> paymentMethods)
{
    Console.WriteLine("Update a value of an item by Key:");

    var key = 3;

    Console.WriteLine($"Value of Key {key} before the update: {paymentMethods[key]}");

    // Update the value of the item with Key number 3:
    paymentMethods[key] = "Apple Pay";

    Console.WriteLine($"Value of Key {key} after the update: {paymentMethods[key]}");

    Console.WriteLine();
}

static void RecriateItem(Dictionary<int, string> paymentMethods)
{
    Console.WriteLine("Recriate item:");

    // "Update" a Key in a dictionary: it's not possible to perform an update operation in a Dictionary, however
    // it's possible to achieve something similar by adding a new key-value pair and removing the previous one:
    var keyToBeRecriated = 3;
    var newKeyToBeCreated = 5;

    if (paymentMethods.ContainsKey(keyToBeRecriated))
    {
        // Step 1: Add a new key-value pair with the updated key:
        paymentMethods[newKeyToBeCreated] = paymentMethods[keyToBeRecriated];

        // Step 2: Remove the old key:
        paymentMethods.Remove(keyToBeRecriated);
    }
}

static void DictionarySize(Dictionary<int, string> paymentMethods)
{
    var dictionarySize = paymentMethods.Count();

    Console.WriteLine($"Dictionary size: {dictionarySize}");
    Console.WriteLine();
}

static void RemoveDictionaryItem(Dictionary<int, string> paymentMethods)
{
    Console.WriteLine("Remove dictionary item");

    var keyToBeRemoved = 1; // For a existent key;
    // var keyToBeRemoved = 7; // For a non existent key;

    // The operation below returns a boolean value: true when the key-value pair is removed, and false when is not removed:
    var wasRemoved = paymentMethods.Remove(keyToBeRemoved);

    Console.WriteLine($"Removed? {wasRemoved}");

    // Another example:
    var anotherKeyToBeRemoved = 2;
    paymentMethods.Remove(anotherKeyToBeRemoved);

    DisplayDictionaryItems(paymentMethods);
}

static void TryAddPaymentMethodsConcurrentDictionary(ConcurrentDictionary<int, string> paymentMethodsConcurrentDictionary)
{
    Console.WriteLine("TryAddPaymentMethods ConcurrentDictionary");

    paymentMethodsConcurrentDictionary.TryAdd(1, "Credit Card");
    paymentMethodsConcurrentDictionary.TryAdd(2, "PayPal");
    paymentMethodsConcurrentDictionary.TryAdd(3, "Google Pay");
}

static void DisplayDictionaryItemsConcurrentDictionary(ConcurrentDictionary<int, string> paymentMethodsConcurrentDictionary)
{
    foreach (var paymentMethod in paymentMethodsConcurrentDictionary)
    {
        Console.WriteLine($"Key: {paymentMethod.Key}, Value: {paymentMethod.Value}");
    }

    Console.WriteLine();
}

static void TryGetConcurrentDictionary(ConcurrentDictionary<int, string> paymentMethodsConcurrentDictionary)
{
    Console.WriteLine("TryGet ConcurrentDictionary:");

    var existentKey = 2;

    if (paymentMethodsConcurrentDictionary.TryGetValue(existentKey, out string valueForExistingKey))
    {
        Console.WriteLine($"Key {existentKey} was found. Value: {valueForExistingKey}");
    }
    else
    {
        Console.WriteLine($"Key {existentKey} was not found");
    }

    var nonExistentKey = 6;

    if (paymentMethodsConcurrentDictionary.TryGetValue(nonExistentKey, out string valueForNonExistingKey))
    {
        Console.WriteLine($"Updated Value for {nonExistentKey}: {valueForNonExistingKey}");
    }
    else
    {
        Console.WriteLine($"Key {nonExistentKey} was not found\n");
    }
}

void AddOrUpdateItemConcurrentDictionary(ConcurrentDictionary<int, string> paymentMethodsConcurrentDictionary)
{
    Console.WriteLine("AddOrUpdateItems ConcurrentDictionary");

    // Use AddOrUpdate to add a new key-value pair or update an existing one;

    // In this example, the key-value pair with Key 3 will be updated:
    var keyToBeUpdated = 3;
    paymentMethodsConcurrentDictionary.AddOrUpdate(
        keyToBeUpdated,
        "Apple Pay (Added)",     // Value to add if the the key does not exist;
        (key, oldValue) => "Apple Pay (Updated)"  // Update the existing value;
    );

    // In this example, a new key-value pair with Key 4 will be added:
    var keyToBeAdded = 4;
    paymentMethodsConcurrentDictionary.AddOrUpdate(
        keyToBeAdded,
        "Cash (Added)",     // Value to add since the key does not exist;
        (key, oldValue) => oldValue  // No update is performed in this case;
    );

    // Another example:
    //var keyToAddOrUpdate = 1; // to perform an update;
    var keyToAddOrUpdate = 5;   // to perform an insertion;
    var paymentMethod = "Bitcoin";

    paymentMethodsConcurrentDictionary.AddOrUpdate(
        keyToAddOrUpdate,
        paymentMethod,
        (key, oldValue) => paymentMethod
    );

    // Display the updated dictionary:
    foreach (var keyValuePair in paymentMethodsConcurrentDictionary)
    {
        Console.WriteLine($"Key: {keyValuePair.Key}, Value: {keyValuePair.Value}");
    }

    Console.WriteLine();
}

void TryRemoveConcurrentDictionary(ConcurrentDictionary<int, string> paymentMethodsConcurrentDictionary)
{
    Console.WriteLine("TryRemove ConcurrentDictionary");

    var keyToBeRemoved = 2;

    var keyRemoved = paymentMethodsConcurrentDictionary.TryRemove(keyToBeRemoved, out string removedValue);

    if (keyRemoved)
    {
        Console.WriteLine($"The Key {keyToBeRemoved} was removed. Value: {removedValue}");
    }
    else
    {
        Console.WriteLine($"The Key {keyToBeRemoved} was not found");
    }

    // Attempt to remove a key that does not exist, will not thrown error:
    var nonExistentKeyToBeRemoved = 6;

    bool nonExistentKeyRemoved = paymentMethodsConcurrentDictionary.TryRemove(nonExistentKeyToBeRemoved, out string nonExistentValue);

    if (nonExistentKeyRemoved)
    {
        Console.WriteLine($"The Key {nonExistentKeyToBeRemoved} was removed. Value: {nonExistentValue}");
    }
    else
    {
        Console.WriteLine($"The Key {nonExistentKeyToBeRemoved} was not found");
    }
}