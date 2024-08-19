import React from 'react'
import './styles.sass'

function SendingPage({ user, choiceRation }) {
    const [error, setError] = React.useState('');
    const [loading, setLoading] = React.useState(true);

    async function sendData() {
        try {
            await fetch(import.meta.env.VITE_API_URL + ":" + import.meta.env.VITE_PORT + "/api/choise/add-meals", {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json-patch+json'
                },
                body: JSON.stringify(choiceRation.map((day, dayIndex) => {
                    return {
                        appUserId: user.id,
                        dayId: dayIndex + 1,
                        mealsId: day.map(meal => meal.id)
                    }
                }))
            }).then(async res => {
                if (res.ok) {
                    setError(false)
                    setLoading(false)
                } else {
                    throw new Error(await res.text());
                }
            })
        } catch (err) {
            setError(() => err.message);
        }
    }
    React.useEffect(() => {
        sendData()
    }, [])
    return (
        <section className="sending-page main">
            {
                error ?
                    <>
                        <h1 className='title-1'>
                            Ошибка обработки
                        </h1>
                        <img src="/imgs/error.svg" alt="" className='sending-page__success' />
                        <button className='btn-1' onClick={() => {
                            setLoading(true)
                            setError(false)
                            sendData()
                        }}>
                            Повторить
                        </button>
                    </>
                    :
                    (
                        loading ?
                            <>
                                <h1 className="title-1">
                                    Обработка запроса
                                </h1>
                                <img src="/imgs/load.svg" alt="Загрузка" className="sending-page__animation" />
                            </>
                            :
                            <>
                                <h1 className='title-1'>
                                    Заказ принят
                                </h1>
                                <img src="/imgs/success.svg" alt="" className='sending-page__success' />
                            </>
                    )

            }
        </section>
    )
}

export default SendingPage;