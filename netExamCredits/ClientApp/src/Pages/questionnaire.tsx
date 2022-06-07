import React, {useState} from "react";
import './questionnaire.css'
import axios from '../Services/axios'
import ResultElement from "../components/resultElement";
import Result from '../interfaces/Result'

function Questionnaire() {
    const [fio, setFio] = useState<string>();
    const [passportSeries, setPassportSeries] = useState<number>();
    const [passportNumber, setPassportNumber] = useState<number>();
    const [passportGiven, setPassportGiven] = useState<string>();
    const [passportGivenDate, setPassportGivenDate] = useState<Date>();
    const [passportRegistration, setPassportRegistration] = useState<string>();
    const [age, setAge] = useState<number>();
    const [criminalRecord, setCriminalRecord] = useState<boolean>(false);
    const [sum, setSum] = useState<number>();
    const [goal, setGoal] = useState<number>(1);
    const [employment, setEmployment] = useState<number>(1);
    const [otherCredits, setOtherCredits] = useState<boolean>(false);
    const [pledge, setPledge] = useState<number>(1);
    const [autoAge, setAutoAge] = useState<number>();
    
    const [result, setResult] = useState<Result>({
        score: null,
        result: null,
        message: null,
        creditRate: null
    });

    async function handleForm(e: React.FormEvent<HTMLFormElement>) {
        e.preventDefault();
        const data = {
            fio,
            passportSeries,
            passportNumber,
            passportGiven,
            passportGivenDate,
            passportRegistration,
            age,
            criminalRecord,
            sum,
            goal,
            employment,
            otherCredits,
            pledge,
            autoAge
        };
        console.log(data);
        fetch('/CreditDealing/GetCredit',
            {
                method: 'POST',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(data)
            })
            .then(res => res.json())
            .then(res => {
                console.log(setResult(res))
            });
    }

    return <div>
        <h1>Анкета</h1>
        <form onSubmit={handleForm}>
            <p>ФИО:</p>
            <input placeholder='ФИО' required pattern="([А-ЯЁ][а-яё]+[\-\s]?){3,}"
                   onChange={e => setFio(e.target.value)}/>
            <div>
                <p>Паспорт</p>
                <input type='number' placeholder='Серия' required min={1000} max={9999}
                       onChange={e => setPassportSeries(Number.parseInt(e.target.value))}/>
                <input type='number' placeholder='Номер' required min={100000} max={999999}
                       onChange={e => setPassportNumber(Number.parseInt(e.target.value))}/>
                <input placeholder='Кем выдан' required minLength={10} maxLength={40}
                       onChange={e => setPassportGiven(e.target.value)}/>
                <input placeholder='Дата выдачи' type='date' required min='1900-01-01' max='2008-01-01'
                       onChange={e => setPassportGivenDate(new Date(e.target.value))}/>
                <input placeholder='Прописка' required minLength={5} maxLength={40}
                       onChange={e => setPassportRegistration(e.target.value)}/>
            </div>
            <p>Возраст:</p>
            <input type='number' placeholder='Возраст' required min={18} max={100}
                   onChange={e => setAge(Number.parseInt(e.target.value))}/>
            <p>Судимость:</p>
            <input type='checkbox' onChange={e => setCriminalRecord(e.target.value == 'on')}/>
            <p>Сумма кредита:</p>
            <input type='number' step='0.01' placeholder='Сумма кредита' required min={10000} max={100000000}
                   onChange={e => setSum(Number.parseInt(e.target.value))}/>
            <p>Цель:</p>
            <select required onChange={e => setGoal(Number.parseInt(e.target.value))}>
                <option value='1'>Потребительский кредит</option>
                <option value='2'>Недвижимость</option>
                <option value='3'>Перекредитование</option>
            </select>
            <p>Трудоустройство:</p>
            <select required onChange={e => setEmployment(Number.parseInt(e.target.value))}>
                <option value='1'>Безработный</option>
                <option value='2'>ИП</option>
                <option value='3'>Договор ТК РФ</option>
                <option value='4'>Без договора</option>
                <option value='5'>Пенсионер</option>
            </select>
            <p>Другие кредиты:</p>
            <input type='checkbox' onChange={e => setOtherCredits(e.target.value == 'on')}/>
            <p>Залог:</p>
            <select required onChange={e => setPledge(Number.parseInt(e.target.value))}>
                <option value='1'>Недвижимость</option>
                <option value='2'>Автомобиль</option>
                <option value='3'>Поручительство</option>
                <option value='4'>Без залога</option>
            </select>
            <p>Возраст авто в годах, если залог - авто:</p>
            <input type='number' onChange={e => setAutoAge(Number.parseInt(e.target.value))}/>
            <br/>
            <br/>
            <input type='submit' value='Получить кредит'/>
        </form>
        <br/>
        <ResultElement score={result.score} result={result.result} message={result.message} creditRate={result.creditRate}/>
    </div>
}

export default Questionnaire