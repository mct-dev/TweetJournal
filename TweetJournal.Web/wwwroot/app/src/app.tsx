import * as Turbolinks from 'turbolinks'
import React from 'react'
import ReactDOM from 'react-dom'

Turbolinks.start()

const App = () => {
  return (
    <h2>hey buddy</h2>
  )
}

ReactDOM.render(<App/>, document.getElementById('app'))
