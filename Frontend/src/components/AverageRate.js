import React, {useState} from "react"
import {Dropdown} from "react-bootstrap";
import {useNavigate} from "react-router-dom";
var codes = require("currency-codes/data")

export function AverageRate(){
    let navigate = useNavigate()
    async function send(e){
        e.preventDefault()

        if (date === ""){
            setStatus("Error: enter date")
            return
        }

        setStatus("Pending")

        await fetch(process.env.REACT_APP_ASP_LINK+"/average/"+date+"/"+currency.code)
            .then(async (response) => {
                if (response.ok){
                    setStatus(await response.text())
                }
                else if (response.status === 400){
                    setStatus(await response.text())
                }
                else if (response.status === 404){
                    setStatus("No data")
                }
                else if (response.status === 500){
                    navigate("/error")
                }
            })
    }

    const [currency, setCurrency] = useState(codes[0])
    const [status, setStatus] = useState("Result will be here")
    const [date, setDate] = useState(new Date().toISOString().slice(0, 10))
    return (
        <>
            <h1>Average currency rate to PLN</h1>
            <form>
                <div className="form-group">
                    <div className="form-group">
                        <label htmlFor="date-input">Date</label>
                        <input type="date" className="form-control" onChange={e => {setDate(e.target.value)}} value={date} id="date-input" required={true}/>
                    </div>
                    <div className="form-group">
                        <label htmlFor="deadline-input">Currency</label>
                        <Dropdown  className="btn-group">
                            <Dropdown.Toggle type="button" className="btn dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                {currency.code+" ("+currency.currency+")"}
                            </Dropdown.Toggle>
                            <Dropdown.Menu className="dropdown-menu">
                                {codes.map(code => (<Dropdown.Item className="dropdown-item" key={code.number}
                                                   onClick={e => setCurrency(code)}
                                                   active={currency === code.code+"("+code.currency+")"}
                                                   href="#">{code.code} ({code.currency})</Dropdown.Item>
                                ))}
                            </Dropdown.Menu>
                        </Dropdown>
                    </div>
                </div>
                <button type="submit" id="submit" onClick={e => send(e)}
                        className="btn">Check</button>
            </form>
            <h2 className={"result " + (status.toLowerCase().includes("error") || status.toLowerCase().includes("no data")
                ? "error" : "")}>{status}</h2>
        </>
    )
}