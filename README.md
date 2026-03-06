# OneOf.Railway ![Build and tests](https://github.com/Karlos348/OneOf.Railway/actions/workflows/dotnet.yml/badge.svg) [![GitHub](https://img.shields.io/github/license/Karlos348/OneOf.Railway)](licence.md)

Railway Oriented Programming pattern implementation built on top of [OneOf ↗](https://github.com/mcintyre321/OneOf).

The idea is simple: every operation returns either a **Success** or a **Failure**. Once a failure occurs, it propagates through the entire chain automatically — no exceptions, no null checks, no nested `if` statements.

---

## Core types

| Type | Description |
|------|-------------|
| `Result` | Represents either a success or a failure (no value) |
| `Result<T>` | Represents either a success carrying a value of type `T`, or a failure |
| `Failure` | Base failure type with a `Code` string |
| `ValidationFailure` | Failure carrying one or more validation error codes |
| `ExceptionFailure` | Failure wrapping a caught exception |

---

## Creating results

```csharp
// Success
Result ok = ResultFactory.Success();
Result<int> okWithValue = ResultFactory.Success(42);

// Failure
Result fail = ResultFactory.Failure("USER_NOT_FOUND");
Result<int> failWithType = ResultFactory.Failure<int>("USER_NOT_FOUND");

// Specialized failures
Result validationFail = ResultFactory.ValidationFailure("FIELD_REQUIRED", "EMAIL_INVALID");
Result exceptionFail = ResultFactory.ExceptionFailure(ex);
```

---

## Chaining with Bind

`Bind` executes the next step only if the current result is a success. On failure, it short-circuits and propagates the original failure.

```csharp
Result result = ResultFactory.Success()
    .Bind(ValidateInput)
    .Bind(SaveToDatabase)
    .Bind(SendNotification);
```

### Passing values through the chain

```csharp
Result<Order> result = ResultFactory.Success(orderId)
    .Bind(id => FindOrder(id))         // Result<int>  -> Result<Order>
    .Bind(order => ValidateOrder(order)) // Result<Order> -> Result<Order>
    .Bind(order => EnrichOrder(order));  // Result<Order> -> Result<Order>
```

### Async chains

`Bind` works seamlessly with async operations:

```csharp
Result<Invoice> result = await ResultFactory.Success(orderId)
    .Bind(id => FindOrderAsync(id))
    .Bind(order => ValidateOrderAsync(order))
    .Bind(order => GenerateInvoiceAsync(order));
```

Async and sync steps can be mixed freely:

```csharp
Result result = await ResultFactory.Success()
    .Bind(ValidateInput)               // sync
    .Bind(() => SaveToDatabaseAsync()) // async
    .Bind(SendNotification);           // sync
```

---

## Pattern matching with Match

`Match` lets you handle both outcomes and produce a value:

```csharp
string message = result.Match(
    onSuccess: () => "Operation completed.",
    onFailure: failure => $"Error: {failure.Code}"
);
```

With `Result<T>`, the success handler receives the value:

```csharp
string message = result.Match(
    onSuccess: order => $"Order #{order.Id} confirmed.",
    onFailure: failure => $"Error: {failure.Code}"
);
```

Async match from a task:

```csharp
string message = await GetOrderAsync()
    .Match(
        onSuccess: order => order.ToString(),
        onFailure: failure => failure.Code
    );
```

---

## Failure types

### Failure

Base type. Every failure has a `Code`:

```csharp
var failure = new Failure("USER_NOT_FOUND");
failure.Code; // "USER_NOT_FOUND"
```

### ValidationFailure

Carries one or more validation error codes. The base `Code` is always `"CORE_VALIDATION"`:

```csharp
var failure = new ValidationFailure("FIELD_REQUIRED", "EMAIL_INVALID");
failure.Code;  // "CORE_VALIDATION"
failure.Codes; // ["FIELD_REQUIRED", "EMAIL_INVALID"]
failure.IsValidationFailure; // true
```

### ExceptionFailure

Wraps an exception. Useful for catching exceptions at boundaries and converting them to failures:

```csharp
try
{
    return await DoSomethingAsync();
}
catch (Exception ex)
{
    return new ExceptionFailure(ex);
    // or with custom message:
    return new ExceptionFailure(ex, "Failed to process payment.");
}
```

```csharp
var failure = new ExceptionFailure(ex, "Custom message");
failure.Code;         // "CORE_EXCEPTION"
failure.ErrorMessage; // "Custom message"
failure.Exception;    // the original Exception
```

---

## Extracting values

```csharp
// Safe — use TryGetValue
if (result.TryGetValue(out var value))
{
    Console.WriteLine(value);
}

// Unsafe — throws InvalidOperationException on failure
var value = result.GetValue();
```

---

## Real-world example

```csharp
public async Task<Result<OrderConfirmation>> PlaceOrderAsync(PlaceOrderRequest request, CancellationToken ct)
{
    return await ResultFactory.Success(request)
        .Bind(r => ValidateRequest(r))
        .Bind(r => FindCustomerAsync(r.CustomerId, ct))
        .Bind(customer => CheckCreditLimitAsync(customer, request.Amount, ct))
        .Bind(customer => CreateOrderAsync(customer, request, ct))
        .Bind(order => SendConfirmationEmailAsync(order, ct));
}

// At the edge — map to HTTP response
var response = await PlaceOrderAsync(request, ct)
    .Match(
        onSuccess: confirmation => Results.Ok(confirmation),
        onFailure: failure => failure switch
        {
            ValidationFailure vf => Results.BadRequest(vf.Codes),
            _ => Results.Problem(failure.Code)
        }
    );
```
