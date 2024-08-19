import React from 'react'
import './styles.sass'
import QRCode from 'react-qr-code';

function GetQrPage({ prevStage, user }) {
    return (
        <section className="get-qr-page main">
            <QRCode
                size={256}
                style={{ height: "auto", maxWidth: "100%", width: "100%" }}
                value={user.id}
                viewBox={`0 0 256 256`}
            />
            <button className='btn-1' onClick={prevStage}>
                Назад
            </button>
        </section>
    )
}

export default GetQrPage;