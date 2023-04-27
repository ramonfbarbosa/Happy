export type OrphanageRequest = {
    name: string;
    latitude: number;
    longitude: number;
    instructions: string;
    about: string;
    whatsapp: string,
    abreAs: string;
    fechaAs: string;
    openOnWeekends: boolean;
    images: Array<{
        id: number;
        url: string;
    }>
}