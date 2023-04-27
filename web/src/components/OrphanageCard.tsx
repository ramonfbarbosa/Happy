import React from 'react';
import 'bootstrap/dist/css/bootstrap.css'
import '../styles/components/OrphanageCard.css';
import { Link } from 'react-router-dom';
import { ToggleOrphanageRequest } from '../types/ToggleOrphanageRequest';
import api from '../services/api';
import { getAuthData } from '../utils/requests';
import Swal from 'sweetalert2';

function formatPhoneNumber(phoneNumberString: string) {
    var cleaned = ('' + phoneNumberString).replace(/\D/g, '');
    var match = cleaned.match(/^(\d{2})(\d{2})(\d{5})(\d{4})$/);
    if (match) {
        return '+' + match[1] + ' (' + match[2] + ') ' + match[3] + '-' + match[4];
    } 
    return null;
}

const HandleClick = (orphanage: ToggleOrphanageRequest) => {
    const config = {
        headers: {
            Authorization: `Bearer ` + getAuthData().token
        }
    };
    api
        .post("orphanage/toggle-orphanage", JSON.stringify({ "id": orphanage.id }), config)
        .then((res) => {
            Swal.fire(
                'Status do orfanato atualizado com sucesso!',
                '',
                'success'
            )
            setTimeout(function () {
                window.location.reload();
            }, 4000);
        })
        .catch((error) => {
            Swal.fire({
                icon: 'error',
                title: 'Ocorreu um erro inesperado!',
                text: 'Não foi possivel trocar o status do orfanato!'
            })
        });
}

const OrphanageCard = (orphanage: ToggleOrphanageRequest) => {

    const isActive = orphanage.status != true
    let status;
    let ativar;

    if (isActive) {
        status = <h5 className='card-text mb-2 text-muted'>STATUS: DESATIVADO</h5>;
        ativar = <a href="#" className="btn btn-success" onClick={() => HandleClick(orphanage)}>ATIVAR</a>
    }
    if (!isActive) {
        status = <h5 className='card-text mb-2 text-muted'>STATUS: ATIVADO</h5>;
        ativar = <a href="#" className="btn btn-danger" onClick={() => HandleClick(orphanage)}>DESATIVAR</a>
    }

    return (
        <div className="card bg-light mx-2 my-2" style={{ width: '17rem' }}>
            <div className="card-header" >
                <h3 className='card-title'>ORFANATO Nº #{orphanage.id}</h3>
            </div>
            <div className='card-body text-center'>
                <h5 className='card-text mb-2 text-muted'>{orphanage.name}</h5>
                <h5 className='card-text mb-2 text-muted'>{formatPhoneNumber(orphanage.whatsapp)}</h5>
                <h5 className='card-text mb-2 text-muted'>Abre às: {orphanage.abreAs}h</h5>
                <h5 className='card-text mb-2 text-muted'>Fecha às: {orphanage.fechaAs}h</h5>
                {status}
                <Link to={`/orphanages/${orphanage.id}`}>
                    <a className="card-link btn btn-secondary btn-sm">Mais informacoes</a>
                </Link>
                <hr />
                {ativar}
            </div>
        </div>
    );
}

export default OrphanageCard;