// https://dev.to/choc13/grokking-monads-in-f-3j7f

type UserId = UserId of string
type TransactionId = TransactionId of string

type CreditCard = {
    Number : string
    Expiry : string
    Cvv : string
}

type User = {
    Id : UserId
    CreditCatd: CreditCard option
}

let chargeCard (amount: double) (card: CreditCard): TransactionId option
