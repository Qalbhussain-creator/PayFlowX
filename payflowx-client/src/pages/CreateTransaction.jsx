import { useState } from 'react'

function CreateTransaction() {
  const [amount, setAmount] = useState('')
  const [currency, setCurrency] = useState('GBP')
  const [message, setMessage] = useState('')

  const handleSubmit = async (e) => {
    e.preventDefault()

    const newTransaction = {
      amount: Number(amount),
      currency: currency
    }

    try {
      const response = await fetch(
        'https://localhost:7191/api/Transactions/PostRecods',
        {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json'
          },
          body: JSON.stringify(newTransaction)
        }
      )

      if (!response.ok) {
        throw new Error(`HTTP error: ${response.status}`)
      }

      setMessage('Transaction created successfully')
      setAmount('')
      setCurrency('GBP')
    } catch (error) {
      console.error('Error creating transaction:', error)
      setMessage('Failed to create transaction')
    }
  }

  return (
    <div>
      <h1>Create Transaction</h1>

      <form onSubmit={handleSubmit}>
        <div>
          <label>Amount</label>
          <br />
          <input
            type="number"
            value={amount}
            onChange={(e) => setAmount(e.target.value)}
            required
          />
        </div>

        <br />

        <div>
          <label>Currency</label>
          <br />
          <select
            value={currency}
            onChange={(e) => setCurrency(e.target.value)}
          >
            <option value="GBP">GBP</option>
            <option value="USD">USD</option>
            <option value="EUR">EUR</option>
          </select>
        </div>

        <br />

        <button type="submit">
          Create Transaction
        </button>
      </form>

      {message && <p>{message}</p>}
    </div>
  )
}

export default CreateTransaction