import React from "react"
import {AverageRate} from "./components/AverageRate"
import { Route, Routes} from "react-router-dom";
import {InternalError} from "./components/500";
import {NotFound} from "./components/404";
import {NavMenu} from "./components/NavMenu";
import {Footer} from "./components/Footer";
import {MinMaxRate} from "./components/MinMaxRate";
import {MajorDifference} from "./components/MajorDifference";

function App() {
  return (
        <>
            <NavMenu />
            <main>
                <Routes>
                    <Route exact path='/' element={<AverageRate/>} />
                    <Route exact path='/average' element={<AverageRate />} />
                    <Route exact path='/minmax' element={<MinMaxRate />} />
                    <Route exact path='/major-difference' element={<MajorDifference />} />
                    <Route exact path='/error' element={<InternalError />}/>
                    <Route path='*' element={<NotFound />} />
                </Routes>
            </main>
            <Footer />
        </>
  );
}

export default App;
