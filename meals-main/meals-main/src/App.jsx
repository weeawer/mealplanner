import React from 'react'
import './assets/styles/main.sass'
import LoginPage from './pages/login';
import MealsPage from './pages/meals';
import SendingPage from './pages/sending';
import ControlPage from './pages/control';
import GetQrPage from './pages/getQr';

function App() {
  const localUserId = localStorage.getItem('user_id');
  const [stage, setStage] = React.useState(localUserId ? 1 : 0);
  const [user, setUser] = React.useState(localUserId ? { id: localUserId } : {});
  const [choiceRation, setChoiceRation] = React.useState(
    [
      [], [], [], [], []
    ]
  );

  const stages = [
    <LoginPage nextStage={() => { setStage(1) }} setUser={setUser} />,
    <ControlPage setStage={setStage} />,
    <GetQrPage prevStage={() => { setStage(1) }} user={user} />,
    <MealsPage nextStage={() => { setStage(4) }} choiceRation={choiceRation} setChoiceRation={setChoiceRation} />,
    <SendingPage user={user} choiceRation={choiceRation} />
  ]

  return (
    <>
      {
        stages[stage]
      }
    </>
  )
}

export default App
