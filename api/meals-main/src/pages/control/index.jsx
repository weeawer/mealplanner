import React from 'react'
import './styles.sass'

function ControlPage({ setStage }) {
    return (
        <section className="control-page main">
            <button className='btn-1-a' onClick={() => {
                setStage(3)
            }}>
                Выбрать рацион
            </button>
            <button className='btn-1-a' onClick={() => {
                setStage(2)
            }}>
                Получить QR
            </button>
            <button className='btn-1-a red' onClick={() => {
                localStorage.clear();
                window.location.reload();
            }}>
                Выйти
            </button>
        </section>
    )
}

export default ControlPage;