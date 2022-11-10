namespace Pdam.Common.Shared.State;

/// <summary>
/// action for approval state
/// </summary>
public enum ActionState
{
    /// <summary>
    /// disetujui
    /// </summary>
    Approve = 1,
    /// <summary>
    /// revisi/dikembalikan
    /// </summary>
    Revise,
    /// <summary>
    /// ditolak
    /// </summary>
    Reject,
    /// <summary>
    /// update data, status tidak berubah
    /// </summary>
    Update
}