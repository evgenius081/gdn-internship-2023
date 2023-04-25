import React, {useState} from "react"
import {Dropdown} from "react-bootstrap";
import {useNavigate} from "react-router-dom";
var codes = require("currency-codes/data")

export function MajorDifference(){
    let navigate = useNavigate()
    async function send(e){
        e.preventDefault()

        if (quotationNumber < 1 || quotationNumber > 255){
            setStatus("Error: quotation number should be in range 1 to 255")
            return
        }

        setStatus("Pending")

        await fetch(process.env.REACT_APP_ASP_LINK+"/majordiff/"+currency.code+"/"+quotationNumber)
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

    const [status, setStatus] = useState("Result will be here")
    const [currency, setCurrency] = useState(codes[0])
    const [quotationNumber, setQuotationNumber] = useState(1)
    return (
        <>
            <h1>Major difference between currency rates to PLN</h1>
            <form>
                <div className="form-group">
                    <div className="form-group">
                        <label htmlFor="deadline-input">Currency</label>
                        <Dropdown  className="btn-group">
                            <Dropdown.Toggle type="button" className="btn dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                {currency.code+" ("+currency.currency+")"}
                            </Dropdown.Toggle>
                            <Dropdown.Menu className="dropdown-menu">
                                {codes.map(code => (<Dropdown.Item className="dropdown-item" key={code.number}
                                                                   onClick={e =>
                                                                       setCurrency(code)}
                                                                   active={currency === code.code+"("+code.currency+")"}
                                                                   href="#">{code.code} ({code.currency})</Dropdown.Item>
                                ))}
                            </Dropdown.Menu>
                        </Dropdown>
                    </div>
                    <div className="form-group">
                        <label htmlFor="date-input">Quotation Number</label>
                        <input type="number" className="form-control" id="quoattion-input"
                               onChange={e => setQuotationNumber(e.target.value)}
                               value={quotationNumber} min={1} max={255} required={true}/>
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