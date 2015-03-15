/*****************************************************************
 * MPAPI - Message Passing API
 * A framework for writing parallel and distributed applications
 * 
 * Author   : Frank Thomsen
 * Web      : http://sector0.dk
 * Contact  : mpapi@sector0.dk
 * License  : New BSD licence
 * 
 * Copyright (c) 2008, Frank Thomsen
 * 
 * Feel free to contact me with bugs and ideas.
 *****************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace MPAPI
{
    [Serializable]
    public enum MessageLevel
    {
        /// <summary>
        /// The message is a system message.
        /// </summary>
        System,
        /// <summary>
        /// The message is a user message - it is sent directly from a worker.
        /// </summary>
        User
    }

    public sealed class Message
    {
        private MessageLevel _messageLevel;
        private WorkerAddress _receiverAddress;
        private WorkerAddress _senderAddress;
        private int _messageType;
        private object _content;

        public Message(MessageLevel messageLevel, WorkerAddress receiverAddress, WorkerAddress senderAddress, int messageType, object content)
        {
            _messageLevel = messageLevel;
            _receiverAddress = receiverAddress;
            _senderAddress = senderAddress;
            _messageType = messageType;
            _content = content;
        }

        #region Properties
        /// <summary>
        /// Gets the leve of this message - either User or System.
        /// </summary>
        public MessageLevel MessageLevel { get { return _messageLevel; } }

        /// <summary>
        /// Gets the address of the receiver of this message.
        /// </summary>
        public WorkerAddress ReceiverAddress { get { return _receiverAddress; } }

        /// <summary>
        /// Gets the addres of the sender of this message.
        /// </summary>
        public WorkerAddress SenderAddress { get { return _senderAddress; } }

        /// <summary>
        /// Gets the message type of this message.
        /// </summary>
        public int MessageType { get { return _messageType; } }

        /// <summary>
        /// Gets the contents of this message.
        /// </summary>
        public object Content { get { return _content; } }
        #endregion
    }
}
