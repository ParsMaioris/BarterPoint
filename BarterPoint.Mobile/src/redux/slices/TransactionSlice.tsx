import {createSlice, PayloadAction} from '@reduxjs/toolkit'
import {TransactionHistory} from '../../models/TransactionHistory'
import {getAllTransactions, getUserTransactions} from '../../api/ApiService'

interface TransactionState
{
    all: TransactionHistory[]
    user: TransactionHistory[]
    loading: boolean
    error: string | null
}

const initialState: TransactionState = {
    all: [],
    user: [],
    loading: false,
    error: null
}

const transactionSlice = createSlice({
    name: 'transaction',
    initialState,
    reducers: {},
    extraReducers: (builder) =>
    {
        builder
            .addCase(getAllTransactions.pending, (state) =>
            {
                state.loading = true
                state.error = null
            })
            .addCase(getAllTransactions.fulfilled, (state, action: PayloadAction<TransactionHistory[]>) =>
            {
                state.loading = false
                state.all = action.payload
            })
            .addCase(getAllTransactions.rejected, (state, action) =>
            {
                state.loading = false
                state.error = action.payload as string
            })
            .addCase(getUserTransactions.pending, (state) =>
            {
                state.loading = true
                state.error = null
            })
            .addCase(getUserTransactions.fulfilled, (state, action: PayloadAction<TransactionHistory[]>) =>
            {
                state.loading = false
                state.user = action.payload
            })
            .addCase(getUserTransactions.rejected, (state, action) =>
            {
                state.loading = false
                state.error = action.payload as string
            })
    }
})

export default transactionSlice.reducer