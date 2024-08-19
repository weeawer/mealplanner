import React from 'react';
import './styles.sass'

const weekSlugs = [
    'Понедельник',
    'Вторник',
    'Среда',
    'Четверг',
    'Пятница',
    'Суббота',
    'Воскресенье'
]

const weekSlugsShort = [
    'ПН',
    'ВТ',
    'СР',
    'ЧТ',
    'ПТ',
    'СБ',
    'ВС'
]

function MealsPage({ nextStage, choiceRation, setChoiceRation }) {
    const [weekRation, setWeekRation] = React.useState(null);
    const [activeDay, setActiveDay] = React.useState(1);

    React.useEffect(() => {
        async function getWeekRation() {
            const data = await fetch(
                import.meta.env.VITE_API_URL + ":" + import.meta.env.VITE_PORT + "/api/category"
            ).then(res => res.json());

            setWeekRation(data);
        }

        getWeekRation();
    }, [])

    let activeDayMenu = [];

    if (weekRation) {
        const unsortedDayMenu = weekRation.ration.find((day) => day.id == activeDay);
        let categories = unsortedDayMenu.meals.reduce((categoriesTemp, meal, mealIndex) => {
            if (!categoriesTemp.includes(meal.categoryId)) {
                categoriesTemp.push(meal.categoryId);
            }

            return categoriesTemp;
        }, []);

        activeDayMenu = categories.map(categoryId => {
            return {
                categoryId: categoryId,
                category_name: weekRation.categories.find(dataCategory => dataCategory.id == categoryId).name,
                meals: unsortedDayMenu.meals.filter(meal => meal.categoryId == categoryId)
            }
        });
    }

    return (
        <section className='meals-page main'>
            <header className='meals-page__header'>
                <img src="/imgs/logo.svg" alt="Логотип" className='meals-page__logo' />
                <h1 className='title-1'>
                    Меню ({weekRation ? weekRation.start_date : ''})
                </h1>
            </header>
            <aside className='meals-page__navigation'>
                {
                    weekRation ? weekRation.ration.map((day) => {
                        function handleDayClick() {
                            setActiveDay(day.id)
                        }

                        return (
                            <>
                                <div
                                    onClick={handleDayClick}
                                    className={'day' + (day.id == activeDay ? " active" : "")}
                                    key={'day' + day.id}
                                >
                                    <h2 className='title-2'>
                                        {
                                            weekSlugs[day.id - 1]
                                        }
                                    </h2>
                                </div>
                                {/* <div
                                    onClick={handleDayClick}
                                    className={'day day__mobile' + (day.id == activeDay ? " active" : "")}
                                    key={'day_mobile' + day.id}
                                >
                                    <h2 className='title-2'>
                                        {
                                            weekSlugsShort[day.id - 1].slice(0, 2)
                                        }
                                    </h2>
                                </div> */}
                            </>
                        )
                    }) : <></>
                }
                <div className='meals-page__control'>
                    <p>
                        <span>Итог: </span>
                        {
                            choiceRation.reduce((sum, day) => {
                                if (day) {
                                    sum += day.reduce((daySum, meal) => daySum + meal.price, 0)
                                }

                                return sum
                            }, 0)
                        }
                        <span>₽</span>
                    </p>

                    <button
                        className='btn-1'
                        onClick={() => {
                            const dataForRequest = choiceRation.map((day, dayIndex) => {
                                return {
                                    day_id: dayIndex + 1,
                                    meals: day
                                }
                            })

                            nextStage();

                            console.log(JSON.stringify(dataForRequest))
                        }}
                    >
                        Подтвердить
                    </button>
                </div>
            </aside>
            <div className='meals-page__content'>
                {
                    activeDayMenu.map(tile => {
                        return (
                            <div className='category' key={'category' + tile.categoryId}>
                                <h3 className='title-3'>
                                    {tile.category_name}
                                </h3>
                                <div className='category__list'>
                                    {
                                        tile.meals.map(meal => {
                                            return (
                                                <div
                                                    className={'meal-card' + (choiceRation[activeDay - 1].find(mealFind => mealFind.id == meal.id) ? " active" : '')}
                                                    onClick={() => {
                                                        let newRation = [...choiceRation];
                                                        if (newRation[activeDay - 1].find(mealFind => mealFind.id == meal.id)) {
                                                            newRation[activeDay - 1] = newRation[activeDay - 1].filter(mealFilt => mealFilt.id !== meal.id);
                                                        } else {
                                                            newRation[activeDay - 1].push(meal)
                                                        }

                                                        setChoiceRation(newRation);
                                                    }}
                                                    key={'meal' + tile.categoryId + meal.id}
                                                >
                                                    <h4 className='title-4'>
                                                        {
                                                            meal.name
                                                        }
                                                    </h4>
                                                    <p className='meal-card__descr'>
                                                        {
                                                            meal.description
                                                        }
                                                    </p>
                                                    <p className='meal-card__price'>
                                                        {
                                                            meal.price + ' ₽'
                                                        }
                                                    </p>
                                                </div>
                                            )
                                        })
                                    }
                                </div>
                            </div>
                        )
                    })
                }
            </div>
        </section>
    );
}

export default MealsPage;