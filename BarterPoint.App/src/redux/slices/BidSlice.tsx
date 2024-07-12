import {createSlice, PayloadAction} from "@reduxjs/toolkit"
import {Bid} from "../../models/Bid"
import {addBid, fetchAllBids, removeBid} from "../../api/ApiService"

interface BidState
{
    bids: Bid[]
    loading: boolean
    error: string | null
}

const initialBidState: BidState = {
    bids: [],
    loading: false,
    error: null,
}

const bidSlice = createSlice({
    name: "bids",
    initialState: initialBidState,
    reducers: {},
    extraReducers: (builder) =>
    {
        builder
            .addCase(fetchAllBids.pending, (state) =>
            {
                state.loading = true
                state.error = null
            })
            .addCase(fetchAllBids.fulfilled, (state, action: PayloadAction<Bid[]>) =>
            {
                state.bids = action.payload
                state.loading = false
            })
            .addCase(fetchAllBids.rejected, (state, action) =>
            {
                state.loading = false
                state.error = action.payload as string
            })
            .addCase(addBid.pending, (state) =>
            {
                state.loading = true
                state.error = null
            })
            .addCase(addBid.fulfilled, (state, action: PayloadAction<number>) =>
            {
                state.loading = false
            })
            .addCase(addBid.rejected, (state, action) =>
            {
                state.loading = false
                state.error = action.payload as string
            })
            .addCase(removeBid.pending, (state) =>
            {
                state.loading = true
                state.error = null
            })
            .addCase(removeBid.fulfilled, (state, action: PayloadAction<number>) =>
            {
                state.bids = state.bids.filter(bid => bid.id !== action.payload)
                state.loading = false
            })
            .addCase(removeBid.rejected, (state, action) =>
            {
                state.loading = false
                state.error = action.payload as string
            })
    },
})

export default bidSlice.reducer