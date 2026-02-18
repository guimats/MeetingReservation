import type { Reservation } from '../types/reservation';

interface Props {
    reservas: Reservation[];
}

export function ReservationList({ reservas }: Props) {
    return (
        <div>
            <h3>Próximas Reservas</h3>
            <ul>
                {reservas.map(reserva => (
                    <li key={reserva.id}>
                        <strong>{reserva.roomName}</strong> - {reserva.requester} 
                        <span style={{ color: reserva.status === 'Confirmada' ? 'green' : 'orange' }}>
                            ({reserva.status})
                        </span>
                    </li>
                ))}
            </ul>
        </div>
    );
}