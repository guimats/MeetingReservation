export interface Room {
    id: number;
    name: string;
    capacity: number;
    localization: string;
}

export interface Reservation {
    id: number;
    roomId: number;
    roomName: string;
    requester: string;
    start: Date;
    end: Date;
    status: 'Confirmada' | 'Pendente' | 'Cancelada';
}