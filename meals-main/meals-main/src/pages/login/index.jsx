import React from 'react';
import './styles.sass';

function LoginPage({ nextStage, setUser }) {
    const [error, setError] = React.useState('');

    const [loginVal, setLoginVal] = React.useState('');
    const [passwordVal, setPasswordVal] = React.useState('');

    async function login() {
        setError(() => '');
        try {
            await fetch(import.meta.env.VITE_API_URL + ":" + import.meta.env.VITE_PORT + "/api/account/login", {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json-patch+json'
                },
                body: JSON.stringify({
                    username: loginVal,
                    password: passwordVal
                })
            }).then(async r => {
                if (r.ok) {
                    const data = await r.json()
                    localStorage.setItem('user_id', data.id);
                    setUser(data)
                    nextStage()
                } else {
                    throw new Error(await r.text());
                }
            })
        } catch (err) {
            setError(() => err.message);
        }
    }

    return (
        <section className='login-page main'>
            <img src="/imgs/logo.svg" alt="" className='login-page__logo' />
            {/* <h1 className='title-1'>
                Вход
            </h1> */}
            <div className="login-page__inputs">
                <input
                    className='input-1'
                    type="text"
                    placeholder="Логин"
                    value={loginVal}
                    onChange={e => setLoginVal(e.currentTarget.value)}
                />
                <input
                    className='input-1'
                    type="password"
                    placeholder="Пароль"

                    value={passwordVal}
                    onChange={e => setPasswordVal(e.currentTarget.value)}
                />
            </div>
            <button
                className='btn-1'
                onClick={login}
            >
                Войти
            </button>
            <p className={'login-page__error' + (error != '' ? ' active' : '')}>
                {
                    (error !== '' ? error : "-")
                }
            </p>
        </section>
    );
}

export default LoginPage;