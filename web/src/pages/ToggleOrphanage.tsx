import React, { useEffect, useState } from "react";
import 'bootstrap/dist/css/bootstrap.css'
import { ToggleOrphanageRequest } from "../types/ToggleOrphanageRequest";
import OrphanageCard from "../components/OrphanageCard";
import { BASE_URL } from "../utils/requests";
import { AxiosRequestConfig } from "axios";
import axios from 'axios';
import { Link } from "react-router-dom";

const ToggleOrphanage = () => {

    const [orphanages, setOrphaanges] = useState<ToggleOrphanageRequest[]>([]);

    useEffect(() => {
        const params: AxiosRequestConfig = {
            method: 'GET',
            url: `/orphanage/orphanages`,
            baseURL: BASE_URL
        };
        axios(params)
            .then((response) => {
                setOrphaanges(response.data);
            })
    }, []);

    return (
        <main>
            <div className="container my-4">
                <div>
                    <h1 style={{ color: "#212529" }}>Ative ou desative orfanatos</h1>
                </div>
                <div className="card text-center">
                    <div className="card-header">
                        <ul className="nav nav-tabs card-header-tabs">
                            <li className="nav-item">
                                <a className="nav-link active" href="">Orfanatos</a>
                            </li>
                        </ul>
                    </div>
                    <div className="card-body">
                        {orphanages.map((orphanage) => {
                            return (
                                <OrphanageCard
                                    id={orphanage.id}
                                    name={orphanage.name}
                                    whatsapp={orphanage.whatsapp}
                                    abreAs={orphanage.abreAs}
                                    fechaAs={orphanage.fechaAs}
                                    status={orphanage.status} />
                            );
                        })}
                    </div>
                    <div>
                        <hr />
                        <br />
                        <Link to={`/app`}>
                            <button className="btn btn-outline-primary btn-lg btn-block" style={{ color: "#00000" }}>Clique para voltar ao mapa</button>
                        </Link>
                        <br />
                        <br />
                    </div>
                </div>
            </div>
        </main>
    );
}

export default ToggleOrphanage;