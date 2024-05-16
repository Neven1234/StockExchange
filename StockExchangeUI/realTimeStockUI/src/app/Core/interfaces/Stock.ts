export interface Stock{
    symbol:string;
    currency:string
    regularMarketPrice:number
    fiftyTwoWeekHigh :number
    fiftyTwoWeekLow :number
    regularMarketDayHigh :number
    regularMarketDayLow :number
    regularMarketVolume:number
    chartPreviousClose :number
    timestamp :number[]
}