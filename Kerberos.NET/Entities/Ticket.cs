﻿using Kerberos.NET.Crypto;

namespace Kerberos.NET.Entities
{
    public class Ticket
    {
        public Ticket(Asn1Element element)
        {
            var childNode = element[0][0];

            for (var i = 0; i < childNode.Count; i++)
            {
                var node = childNode[i];

                switch (node.ContextSpecificTag)
                {
                    case 0:
                        TicketVersionNumber = node[0].AsInt();
                        break;

                    case 1:
                        Realm = node[0].AsString();
                        break;

                    case 2:
                        SName = new PrincipalName(node, Realm);
                        break;

                    case 3:
                        EncPart = new EncryptedData(node);
                        break;
                }
            }
        }

        public int TicketVersionNumber { get; private set; }

        public string Realm { get; private set; }

        public PrincipalName SName { get; private set; }

        public EncryptedData EncPart { get; private set; }
    }
}
