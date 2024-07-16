import {createSlice, PayloadAction} from '@reduxjs/toolkit'
import {User} from '../../models/User'
import {getUserFavorites, registerUser, removeFavorite, signInUser} from '../../api/ApiService'
import AsyncStorage from '@react-native-async-storage/async-storage'

interface UserState
{
    currentUser: {id: string}
    users: User[]
    status: 'idle' | 'loading' | 'succeeded' | 'failed'
    error: string | null
    userId: string | null
    favorites: Favorite[]
}

const initialState: UserState = {
    currentUser: {id: ''},
    users: [],
    status: 'idle',
    error: null,
    userId: null,
    favorites: [],
}

const userSlice = createSlice({
    name: 'users',
    initialState,
    reducers: {
        setCurrentUser: (state, action: PayloadAction<User>) =>
        {
            state.currentUser = action.payload
        },
        clearCurrentUser: (state) =>
        {
            state.currentUser = {id: ''}
            state.userId = null
            AsyncStorage.removeItem('userId')
        },
        addUser: (state, action: PayloadAction<User>) =>
        {
            state.users.push(action.payload)
        },
        removeUser: (state, action: PayloadAction<string>) =>
        {
            state.users = state.users.filter(user => user.id !== action.payload)
        },
        setToken: (state, action: PayloadAction<string | null>) =>
        {
            state.userId = action.payload
        },
        setUserId: (state, action: PayloadAction<string | null>) =>
        {
            state.userId = action.payload
        },
    },
    extraReducers: (builder) =>
    {
        builder
            .addCase(registerUser.pending, (state) =>
            {
                state.status = 'loading'
                state.error = null
            })
            .addCase(registerUser.fulfilled, (state) =>
            {
                state.status = 'succeeded'
                state.error = null
            })
            .addCase(registerUser.rejected, (state, action) =>
            {
                state.status = 'failed'
                state.error = action.payload || 'Failed to register user'
            })
            .addCase(signInUser.pending, (state) =>
            {
                state.status = 'loading'
                state.error = null
            })
            .addCase(signInUser.fulfilled, (state, action) =>
            {
                state.status = 'succeeded'
                state.error = null
                state.currentUser = {id: action.payload.userId || ''}
                state.userId = action.payload.userId || null
            })
            .addCase(signInUser.rejected, (state, action) =>
            {
                state.status = 'failed'
                state.error = action.payload || 'Failed to sign in user'
            })
            .addCase(getUserFavorites.pending, (state) =>
            {
                state.status = 'loading'
                state.error = null
            })
            .addCase(getUserFavorites.fulfilled, (state, action: PayloadAction<Favorite[]>) =>
            {
                state.status = 'succeeded'
                state.favorites = action.payload
            })
            .addCase(getUserFavorites.rejected, (state, action) =>
            {
                state.status = 'failed'
                state.error = action.payload as string
            })
            .addCase(removeFavorite.pending, (state) =>
            {
                state.status = 'loading'
                state.error = null
            })
            .addCase(removeFavorite.fulfilled, (state, action: PayloadAction<RemoveFavoriteRequest>) =>
            {
                state.status = 'succeeded'
                state.favorites = state.favorites.filter(
                    (favorite) => !(favorite.userId === action.payload.userId && favorite.productId === action.payload.productId)
                )
            }).addCase(removeFavorite.rejected, (state, action) =>
            {
                state.status = 'failed'
                state.error = action.payload as string
            })
    }
})

export const {setCurrentUser, clearCurrentUser, addUser, removeUser, setUserId} = userSlice.actions

export default userSlice.reducer