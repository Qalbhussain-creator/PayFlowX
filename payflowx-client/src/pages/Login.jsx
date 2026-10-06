import { useState } from 'react'

function Login({ onLogin }) {
  const [email, setEmail] = useState('')
  const [password, setPassword] = useState('')
  const [error, setError] = useState('')

  const handleLogin = async (e) => {
    e.preventDefault()
    setError('')

    try {
      const response = await fetch(
        'https://localhost:7191/api/Auth/Login',
        {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json'
          },
          body: JSON.stringify({
            email,
            password
          })
        }
      )

      if (!response.ok) {
        setError('Invalid email or password')
        return
      }

      const data = await response.json()

      localStorage.setItem('token', data.token)

      onLogin()
    } catch (error) {
      console.error('Login error:', error)
      setError('Login failed')
    }
  }

  return (
    <div>
      <h1>PayFlowX Login</h1>

      <form onSubmit={handleLogin}>
        <div>
          <label>Email</label>
          <br />

          <input
            type="email"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
            required
          />
        </div>

        <br />

        <div>
          <label>Password</label>
          <br />

          <input
            type="password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            required
          />
        </div>

        <br />

        <button type="submit">
          Login
        </button>

        {error && <p>{error}</p>}
      </form>
    </div>
  )
}

export default Login