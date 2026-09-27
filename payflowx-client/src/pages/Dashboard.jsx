import { useEffect, useState } from 'react'

function Dashboard() {
  const [transactions, setTransactions] = useState([])

  useEffect(() => {
    fetch('https://localhost:7191/api/Transactions')
      .then(response => response.json())
      .then(data => setTransactions(data))
      .catch(error => console.error(error))
  }, [])

  const totalTransactions = transactions.length

  const activeTransactions = transactions.filter(
    transaction => transaction.status === 'Active'
  ).length

  const totalValue = transactions.reduce(
    (sum, transaction) => sum + transaction.amount,
    0
  )

  return (
    <div>
      <h1>Dashboard</h1>
      <p>Overview of PayFlowX transactions.</p>

      <div className="cards">
        <div className="card">
          <h3>Total Transactions</h3>
          <p>{totalTransactions}</p>
        </div>

        <div className="card">
          <h3>Active Transactions</h3>
          <p>{activeTransactions}</p>
        </div>

        <div className="card">
          <h3>Total Value</h3>
          <p>£{totalValue.toFixed(2)}</p>
        </div>
      </div>
    </div>
  )
}

export default Dashboard