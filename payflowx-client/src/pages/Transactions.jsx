import { useEffect, useState } from 'react'

function Transactions() {
  const [transactions, setTransactions] = useState([])

  const [editingId, setEditingId] = useState(null)
  const [editAmount, setEditAmount] = useState('')
  const [editCurrency, setEditCurrency] = useState('')

  useEffect(() => {
    fetch('https://localhost:7191/api/Transactions')
      .then(response => {
        if (!response.ok) {
          throw new Error(`HTTP error: ${response.status}`)
        }

        return response.json()
      })
      .then(data => {
        console.log('Transactions:', data)
        setTransactions(data)
      })
      .catch(error => {
        console.error('Error loading transactions:', error)
      })
  }, [])

  const deleteTransaction = async (id) => {
    try {
      const response = await fetch(
        `https://localhost:7191/api/Transactions/DeleteRecords/${id}`,
        {
          method: 'DELETE'
        }
      )

      if (!response.ok) {
        throw new Error(`Delete failed: ${response.status}`)
      }

      setTransactions(currentTransactions =>
        currentTransactions.filter(transaction => transaction.id !== id)
      )
    } catch (error) {
      console.error('Error deleting transaction:', error)
      alert('Delete failed')
    }
  }

  const startEdit = (transaction) => {
    setEditingId(transaction.id)
    setEditAmount(transaction.amount)
    setEditCurrency(transaction.currency)
  }

  const updateTransaction = async (transaction) => {
    try {
      const updatedTransaction = {
        ...transaction,
        amount: Number(editAmount),
        currency: editCurrency
      }

      const response = await fetch(
        `https://localhost:7191/api/Transactions/UpdateRecords/${transaction.id}`,
        {
          method: 'PUT',
          headers: {
            'Content-Type': 'application/json'
          },
          body: JSON.stringify(updatedTransaction)
        }
      )

      if (!response.ok) {
        throw new Error(`Update failed: ${response.status}`)
      }

      setTransactions(currentTransactions =>
        currentTransactions.map(item =>
          item.id === transaction.id ? updatedTransaction : item
        )
      )

      setEditingId(null)
    } catch (error) {
      console.error('Error updating transaction:', error)
      alert('Update failed')
    }
  }

  return (
    <div>
      <h1>Transactions</h1>

      <table>
        <thead>
          <tr>
            <th>ID</th>
            <th>Reference</th>
            <th>Amount</th>
            <th>Currency</th>
            <th>Status</th>
            <th>Actions</th>
          </tr>
        </thead>

        <tbody>
          {transactions.length === 0 ? (
            <tr>
              <td colSpan="6">No transactions found</td>
            </tr>
          ) : (
            transactions.map(transaction => (
              <tr key={transaction.id}>
                <td>{transaction.id}</td>

                <td>{transaction.reference || '-'}</td>

                <td>
                  {editingId === transaction.id ? (
                    <input
                      type="number"
                      value={editAmount}
                      onChange={(e) => setEditAmount(e.target.value)}
                    />
                  ) : (
                    transaction.amount
                  )}
                </td>

                <td>
                  {editingId === transaction.id ? (
                    <select
                      value={editCurrency}
                      onChange={(e) => setEditCurrency(e.target.value)}
                    >
                      <option value="GBP">GBP</option>
                      <option value="USD">USD</option>
                      <option value="EUR">EUR</option>
                    </select>
                  ) : (
                    transaction.currency
                  )}
                </td>

                <td>{transaction.status}</td>

                <td>
                  {editingId === transaction.id ? (
                    <button onClick={() => updateTransaction(transaction)}>
                      Save
                    </button>
                  ) : (
                    <button onClick={() => startEdit(transaction)}>
                      Edit
                    </button>
                  )}

                  <button
                    onClick={() => deleteTransaction(transaction.id)}
                  >
                    Delete
                  </button>
                </td>
              </tr>
            ))
          )}
        </tbody>
      </table>
    </div>
  )
}

export default Transactions